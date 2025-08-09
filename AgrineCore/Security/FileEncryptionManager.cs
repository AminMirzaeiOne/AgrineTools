using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AgrineCore.Security
{
    /// <summary>
    /// File encryption manager:
    /// - EncryptFile/DecryptFile with password (PBKDF2 -> AES key + HMAC key)
    /// - EncryptFile/DecryptFile with RSA public/private (hybrid)
    /// Format:
    /// [4 bytes magic "AGC1"][1 byte flags][4 bytes iterations][16 bytes salt]
    /// [4 bytes encKeyLen][encKey bytes if any][16 bytes IV][ciphertext...][32 bytes HMAC]
    /// </summary>
    public static class FileEncryptionManager
    {
        private const string Magic = "AGC1"; // 4 bytes
        private const int SaltSize = 16;
        private const int IvSize = 16;
        private const int AesKeySize = 32; // bytes
        private const int HmacKeySize = 32;
        private const int MacSize = 32; // HMAC-SHA256
        private const uint DefaultIterations = 100000;

        // Flags
        private const byte FLAG_PASSWORD = 0x01;
        private const byte FLAG_RSA = 0x02;

        /// <summary>
        /// Encrypt a file using a password (PBKDF2) or RSA public key (xml).
        /// If password != null -> password mode used.
        /// Else if rsaPublicXml != null -> RSA hybrid used.
        /// </summary>
        public static void EncryptFile(string inputPath, string outputPath, string password = null, string rsaPublicXml = null, uint iterations = DefaultIterations)
        {
            if (string.IsNullOrEmpty(inputPath)) throw new ArgumentNullException("inputPath");
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentNullException("outputPath");
            if (!File.Exists(inputPath)) throw new FileNotFoundException("Input file not found", inputPath);

            bool usePassword = !string.IsNullOrEmpty(password);
            bool useRsa = !string.IsNullOrEmpty(rsaPublicXml);

            if (!usePassword && !useRsa) throw new ArgumentException("Either password or rsaPublicXml must be provided");

            // Prepare keys
            byte[] aesKey = null;
            byte[] hmacKey = null;
            byte[] salt = new byte[SaltSize];
            byte[] encryptedKeyBlob = null;

            if (usePassword)
            {
                // derive keys from password
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                byte[] derived = DeriveKeyFromPassword(password, salt, (int)iterations, AesKeySize + HmacKeySize);
                aesKey = new byte[AesKeySize];
                hmacKey = new byte[HmacKeySize];
                Array.Copy(derived, 0, aesKey, 0, AesKeySize);
                Array.Copy(derived, AesKeySize, hmacKey, 0, HmacKeySize);
                // encryptedKeyBlob remains null (not used)
            }
            else
            {
                // RSA hybrid: generate aes+ hmac keys random, encrypt them with RSA public
                aesKey = EncryptionManager.GenerateAesKey();
                hmacKey = EncryptionManager.GenerateRandomBytes(HmacKeySize);
                // pack keys aes||hmac then RSA encrypt
                byte[] pack = new byte[AesKeySize + HmacKeySize];
                Array.Copy(aesKey, 0, pack, 0, AesKeySize);
                Array.Copy(hmacKey, 0, pack, AesKeySize, HmacKeySize);
                encryptedKeyBlob = EncryptionManager.RsaEncrypt(pack, rsaPublicXml);
            }

            // IV
            byte[] iv = EncryptionManager.GenerateIv();

            // Open output file and write header first (via HMAC stream so header included in MAC)
            FileStream outFs = null;
            FileStream inFs = null;
            HmacWriteStream hmacWriter = null;
            HMACSHA256 hmac = null;
            CryptoStream cryptoStream = null;

            try
            {
                outFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
                // prepare HMAC (will initialize after having hmacKey)
                hmac = new HMACSHA256(hmacKey);

                // Wrap outFs with HmacWriteStream so writes also feed HMAC
                hmacWriter = new HmacWriteStream(outFs, hmac);

                // Build header bytes
                using (var headerMs = new MemoryStream())
                {
                    // magic
                    byte[] magicBytes = Encoding.ASCII.GetBytes(Magic);
                    headerMs.Write(magicBytes, 0, magicBytes.Length);

                    // flags
                    byte flags = 0;
                    if (usePassword) flags |= FLAG_PASSWORD;
                    if (useRsa) flags |= FLAG_RSA;
                    headerMs.WriteByte(flags);

                    // iterations (4 bytes little-endian)
                    uint it = usePassword ? iterations : 0u;
                    headerMs.Write(BitConverter.GetBytes(it), 0, 4);

                    // salt (16 bytes) (if password, value; else zero)
                    if (usePassword) headerMs.Write(salt, 0, SaltSize);
                    else headerMs.Write(new byte[SaltSize], 0, SaltSize);

                    // encryptedKey length & encryptedKey (if RSA)
                    if (useRsa)
                    {
                        int len = encryptedKeyBlob.Length;
                        headerMs.Write(BitConverter.GetBytes(len), 0, 4);
                        headerMs.Write(encryptedKeyBlob, 0, len);
                    }
                    else
                    {
                        headerMs.Write(BitConverter.GetBytes(0), 0, 4);
                    }

                    // IV (16 bytes)
                    headerMs.Write(iv, 0, IvSize);

                    byte[] headerBytes = headerMs.ToArray();

                    // Write header via hmacWriter (so HMAC includes header)
                    hmacWriter.Write(headerBytes, 0, headerBytes.Length);
                    hmacWriter.Flush();
                }

                // Now encrypt input file streaming: use CryptoStream writing to hmacWriter
                inFs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);

                using (var aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = aesKey;
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor();

                    cryptoStream = new CryptoStream(hmacWriter, encryptor, CryptoStreamMode.Write);

                    byte[] buffer = new byte[64 * 1024];
                    int read;
                    while ((read = inFs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, read);
                    }

                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Flush();
                    // after cryptoStream closed, ciphertext has been written and HMAC updated by hmacWriter
                }

                // finalize HMAC
                byte[] finalMac = hmac.Hash;
                if (finalMac == null || finalMac.Length == 0)
                {
                    // in case Hash not computed yet (TransformFinalBlock not invoked), call TransformFinalBlock on empty array
                    hmac.TransformFinalBlock(new byte[0], 0, 0);
                    finalMac = hmac.Hash;
                }

                // Write MAC (not included in MAC)
                outFs.Write(finalMac, 0, finalMac.Length);
                outFs.Flush();
            }
            finally
            {
                if (cryptoStream != null) cryptoStream.Dispose();
                if (hmacWriter != null) hmacWriter.Dispose();
                if (hmac != null) hmac.Dispose();
                if (inFs != null) inFs.Dispose();
                if (outFs != null) outFs.Dispose();
                // zero out keys in memory
                if (aesKey != null) Array.Clear(aesKey, 0, aesKey.Length);
                if (hmacKey != null) Array.Clear(hmacKey, 0, hmacKey.Length);
                if (salt != null) Array.Clear(salt, 0, salt.Length);
                if (encryptedKeyBlob != null) Array.Clear(encryptedKeyBlob, 0, encryptedKeyBlob.Length);
            }
        }

        /// <summary>
        /// Decrypt file with either password or RSA private xml.
        /// </summary>
        public static void DecryptFile(string inputPath, string outputPath, string password = null, string rsaPrivateXml = null)
        {
            if (string.IsNullOrEmpty(inputPath)) throw new ArgumentNullException("inputPath");
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentNullException("outputPath");
            if (!File.Exists(inputPath)) throw new FileNotFoundException("Input file not found", inputPath);

            FileStream inFs = null;
            try
            {
                inFs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                long totalLen = inFs.Length;

                // Read header fields into memory, but also keep header bytes to feed HMAC
                // Read magic (4)
                byte[] buf4 = new byte[4];
                ReadExact(inFs, buf4, 0, 4);
                string magicRead = Encoding.ASCII.GetString(buf4, 0, 4);
                if (!string.Equals(magicRead, Magic, StringComparison.Ordinal))
                    throw new InvalidDataException("Invalid file format (magic mismatch).");

                // flags (1)
                int flagByte = inFs.ReadByte();
                if (flagByte < 0) throw new InvalidDataException("Unexpected EOF while reading flags.");
                byte flags = (byte)flagByte;

                bool usePassword = (flags & FLAG_PASSWORD) != 0;
                bool useRsa = (flags & FLAG_RSA) != 0;

                // iterations (4)
                byte[] itBytes = new byte[4];
                ReadExact(inFs, itBytes, 0, 4);
                uint iterations = BitConverter.ToUInt32(itBytes, 0);

                // salt (16)
                byte[] salt = new byte[SaltSize];
                ReadExact(inFs, salt, 0, SaltSize);

                // encryptedKeyLen (4)
                byte[] encLenBytes = new byte[4];
                ReadExact(inFs, encLenBytes, 0, 4);
                int encKeyLen = BitConverter.ToInt32(encLenBytes, 0);

                // encryptedKey blob
                byte[] encryptedKeyBlob = null;
                if (encKeyLen > 0)
                {
                    encryptedKeyBlob = new byte[encKeyLen];
                    ReadExact(inFs, encryptedKeyBlob, 0, encKeyLen);
                }

                // IV (16)
                byte[] iv = new byte[IvSize];
                ReadExact(inFs, iv, 0, IvSize);

                // Determine header length so we can calculate ciphertext length
                long headerLength = 4 + 1 + 4 + SaltSize + 4 + (encKeyLen > 0 ? encKeyLen : 0) + IvSize;
                long macStartPos = totalLen - MacSize;
                if (macStartPos <= headerLength) throw new InvalidDataException("File too small or corrupted.");

                long cipherLength = macStartPos - headerLength;

                // Reconstruct header bytes EXACTLY as were written to compute HMAC
                // (We already read them piece by piece; to compute HMAC we need the same bytes in same order)
                byte[] headerBytes = new byte[headerLength];
                using (var ms = new MemoryStream())
                {
                    ms.Write(Encoding.ASCII.GetBytes(Magic), 0, 4);
                    ms.WriteByte(flags);
                    ms.Write(itBytes, 0, 4);
                    ms.Write(salt, 0, SaltSize);
                    ms.Write(encLenBytes, 0, 4);
                    if (encKeyLen > 0) ms.Write(encryptedKeyBlob, 0, encKeyLen);
                    ms.Write(iv, 0, IvSize);
                    headerBytes = ms.ToArray();
                }

                // Acquire keys: aesKey + hmacKey
                byte[] aesKey = null;
                byte[] hmacKey = null;

                if (usePassword)
                {
                    if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password required for this file.");
                    byte[] derived = DeriveKeyFromPassword(password, salt, (int)iterations, AesKeySize + HmacKeySize);
                    aesKey = new byte[AesKeySize];
                    hmacKey = new byte[HmacKeySize];
                    Array.Copy(derived, 0, aesKey, 0, AesKeySize);
                    Array.Copy(derived, AesKeySize, hmacKey, 0, HmacKeySize);
                    Array.Clear(derived, 0, derived.Length);
                }
                else if (useRsa)
                {
                    if (string.IsNullOrEmpty(rsaPrivateXml)) throw new ArgumentException("RSA private key required for this file.");
                    byte[] packed = EncryptionManager.RsaDecrypt(encryptedKeyBlob, rsaPrivateXml);
                    if (packed == null || packed.Length != (AesKeySize + HmacKeySize)) throw new CryptographicException("Invalid encrypted key blob.");
                    aesKey = new byte[AesKeySize];
                    hmacKey = new byte[HmacKeySize];
                    Array.Copy(packed, 0, aesKey, 0, AesKeySize);
                    Array.Copy(packed, AesKeySize, hmacKey, 0, HmacKeySize);
                    Array.Clear(packed, 0, packed.Length);
                }
                else
                {
                    throw new InvalidDataException("Unsupported file (no key mode).");
                }

                // Prepare HMAC and compute/verify:
                HMACSHA256 hmac = new HMACSHA256(hmacKey);

                // HMAC must cover headerBytes + ciphertext
                // We'll compute HMAC while decrypting ciphertext (streamed)
                // Create a LimitedStream over input file positioned at headerLength, length cipherLength
                inFs.Position = headerLength;
                SubStream limited = new SubStream(inFs, headerLength, cipherLength);

                // First feed header bytes into HMAC
                hmac.TransformBlock(headerBytes, 0, headerBytes.Length, null, 0);

                // Decrypt ciphertext streaming: read limited via HmacReadStream which updates HMAC with ciphertext bytes,
                // then pass to CryptoStream for decryption and write plaintext to temp file
                string tempOut = Path.GetTempFileName();
                FileStream tempFs = null;
                CryptoStream decryptStream = null;
                try
                {
                    HmacReadStream hmacReader = new HmacReadStream(limited, hmac);

                    using (var aes = Aes.Create())
                    {
                        aes.KeySize = 256;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.Key = aesKey;
                        aes.IV = iv;

                        ICryptoTransform decryptor = aes.CreateDecryptor();
                        decryptStream = new CryptoStream(hmacReader, decryptor, CryptoStreamMode.Read);

                        tempFs = new FileStream(tempOut, FileMode.Create, FileAccess.Write, FileShare.None);

                        byte[] buffer = new byte[64 * 1024];
                        int read;
                        while ((read = decryptStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            tempFs.Write(buffer, 0, read);
                        }
                        tempFs.Flush();
                        tempFs.Close();
                        tempFs = null;

                        // finalize HMAC
                        hmac.TransformFinalBlock(new byte[0], 0, 0);
                        byte[] computedMac = hmac.Hash;
                        if (computedMac == null || computedMac.Length == 0) throw new CryptographicException("HMAC computation failed.");

                        // Read stored MAC from file
                        byte[] storedMac = new byte[MacSize];
                        inFs.Position = headerLength + cipherLength; // should be at macStartPos
                        ReadExact(inFs, storedMac, 0, MacSize);

                        // Compare
                        if (!HashingManager.AreEqual(computedMac, storedMac))
                        {
                            // delete temp
                            try { File.Delete(tempOut); } catch { }
                            throw new CryptographicException("HMAC mismatch - file corrupted or wrong key.");
                        }

                        // HMAC ok -> move tempOut to final outputPath (overwrite if exists)
                        if (File.Exists(outputPath)) File.Delete(outputPath);
                        File.Move(tempOut, outputPath);
                    }
                }
                finally
                {
                    if (decryptStream != null) decryptStream.Dispose();
                    if (tempFs != null) tempFs.Dispose();
                    // clear keys
                    if (aesKey != null) Array.Clear(aesKey, 0, aesKey.Length);
                    if (hmacKey != null) Array.Clear(hmacKey, 0, hmacKey.Length);
                    if (hmac != null) hmac.Dispose();
                }
            }
            finally
            {
                if (inFs != null) inFs.Dispose();
            }
        }

        #region Helpers & small stream wrappers

        private static void ReadExact(Stream s, byte[] buffer, int offset, int count)
        {
            int read = 0;
            while (read < count)
            {
                int r = s.Read(buffer, offset + read, count - read);
                if (r <= 0) throw new EndOfStreamException("Unexpected EOF");
                read += r;
            }
        }

        private static byte[] DeriveKeyFromPassword(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(outputBytes);
            }
        }

        /// <summary>
        /// Stream wrapper that writes to an inner stream and also feeds the HMAC with written bytes.
        /// Used during encryption to compute MAC on header + ciphertext as they are written.
        /// </summary>
        private sealed class HmacWriteStream : Stream
        {
            private readonly Stream _base;
            private readonly HMAC _hmac;

            public HmacWriteStream(Stream baseStream, HMAC hmac)
            {
                _base = baseStream ?? throw new ArgumentNullException("baseStream");
                _hmac = hmac ?? throw new ArgumentNullException("hmac");
            }

            public override bool CanRead { get { return false; } }
            public override bool CanSeek { get { return false; } }
            public override bool CanWrite { get { return _base.CanWrite; } }
            public override long Length { get { return _base.Length; } }
            public override long Position { get { return _base.Position; } set { throw new NotSupportedException(); } }

            public override void Flush()
            {
                _base.Flush();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                // feed HMAC
                _hmac.TransformBlock(buffer, offset, count, null, 0);
                // write bytes
                _base.Write(buffer, offset, count);
            }

            public override void Close()
            {
                base.Close();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    try { _base.Flush(); } catch { }
                    try { _base.Dispose(); } catch { }
                    try { _hmac.Dispose(); } catch { }
                }
                base.Dispose(disposing);
            }

            #region Unsupported read/seek
            public override int Read(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }
            public override long Seek(long offset, SeekOrigin origin) { throw new NotSupportedException(); }
            public override void SetLength(long value) { throw new NotSupportedException(); }
            #endregion
        }

        /// <summary>
        /// Stream wrapper that reads from inner stream and feeds HMAC with the bytes returned by Read.
        /// Used during decryption: HMAC must be computed over ciphertext while reading it.
        /// </summary>
        private sealed class HmacReadStream : Stream
        {
            private readonly Stream _base;
            private readonly HMAC _hmac;

            public HmacReadStream(Stream baseStream, HMAC hmac)
            {
                _base = baseStream ?? throw new ArgumentNullException("baseStream");
                _hmac = hmac ?? throw new ArgumentNullException("hmac");
            }

            public override bool CanRead { get { return _base.CanRead; } }
            public override bool CanSeek { get { return false; } }
            public override bool CanWrite { get { return false; } }
            public override long Length { get { return _base.Length; } }
            public override long Position { get { return _base.Position; } set { throw new NotSupportedException(); } }

            public override void Flush() { _base.Flush(); }

            public override int Read(byte[] buffer, int offset, int count)
            {
                int r = _base.Read(buffer, offset, count);
                if (r > 0)
                {
                    _hmac.TransformBlock(buffer, offset, r, null, 0);
                }
                return r;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    try { _base.Dispose(); } catch { }
                }
                base.Dispose(disposing);
            }

            #region Unsupported write/seek
            public override void Write(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }
            public override long Seek(long offset, SeekOrigin origin) { throw new NotSupportedException(); }
            public override void SetLength(long value) { throw new NotSupportedException(); }
            #endregion
        }

        /// <summary>
        /// Substream (window) over an existing stream: reads start..start+length-1
        /// Useful to limit reading ciphertext region.
        /// Note: does not dispose inner stream.
        /// </summary>
        private sealed class SubStream : Stream
        {
            private readonly Stream _base;
            private readonly long _start;
            private readonly long _length;
            private long _position;

            public SubStream(Stream baseStream, long start, long length)
            {
                _base = baseStream ?? throw new ArgumentNullException("baseStream");
                _start = start;
                _length = length;
                _position = 0;
                _base.Position = _start;
            }

            public override bool CanRead { get { return _base.CanRead; } }
            public override bool CanSeek { get { return true; } }
            public override bool CanWrite { get { return false; } }
            public override long Length { get { return _length; } }
            public override long Position { get { return _position; } set { Seek(value, SeekOrigin.Begin); } }

            public override void Flush() { }

            public override int Read(byte[] buffer, int offset, int count)
            {
                if (_position >= _length) return 0;
                long remaining = _length - _position;
                if (count > remaining) count = (int)remaining;
                int r = _base.Read(buffer, offset, count);
                _position += r;
                return r;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                long target;
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        target = offset;
                        break;
                    case SeekOrigin.Current:
                        target = _position + offset;
                        break;
                    case SeekOrigin.End:
                        target = _length + offset;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("origin");
                }
                if (target < 0 || target > _length) throw new ArgumentOutOfRangeException("offset");
                _position = target;
                _base.Position = _start + _position;
                return _position;
            }

            protected override void Dispose(bool disposing)
            {
                // do not dispose base stream
                base.Dispose(disposing);
            }

            #region Unsupported write/seek
            public override void SetLength(long value) { throw new NotSupportedException(); }
            public override void Write(byte[] buffer, int offset, int count) { throw new NotSupportedException(); }
            #endregion
        }

        #endregion
    }
}
