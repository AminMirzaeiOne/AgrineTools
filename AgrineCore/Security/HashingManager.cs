using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AgrineCore.Security
{
    /// <summary>
    /// Utilities for hashing, HMAC and password hashing (PBKDF2).
    /// - Supports MD5, SHA1, SHA256, SHA384, SHA512
    /// - File hashing (stream-based)
    /// - HMAC generation
    /// - PBKDF2 password hashing: CreatePasswordHash & VerifyPassword
    /// </summary>
    public static class HashingManager
    {
        public enum HashType
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512
        }

        #region Basic hashing (string / bytes / file)

        /// <summary>
        /// Compute hash of a string. Returns hex (default) or base64 if asBase64 == true.
        /// </summary>
        public static string ComputeHash(string text, HashType type = HashType.SHA256, Encoding encoding = null, bool asBase64 = false)
        {
            if (text == null) throw new ArgumentNullException("text");
            encoding = encoding ?? Encoding.UTF8;
            byte[] data = encoding.GetBytes(text);
            byte[] hash = ComputeHashBytes(data, type);
            return asBase64 ? Convert.ToBase64String(hash) : BytesToHex(hash);
        }

        /// <summary>
        /// Compute hash of byte array.
        /// </summary>
        public static byte[] ComputeHashBytes(byte[] data, HashType type = HashType.SHA256)
        {
            if (data == null) throw new ArgumentNullException("data");

            switch (type)
            {
                case HashType.MD5:
                    using (var alg = MD5.Create())
                        return alg.ComputeHash(data);
                case HashType.SHA1:
                    using (var alg = SHA1.Create())
                        return alg.ComputeHash(data);
                case HashType.SHA256:
                    using (var alg = SHA256.Create())
                        return alg.ComputeHash(data);
                case HashType.SHA384:
                    using (var alg = SHA384.Create())
                        return alg.ComputeHash(data);
                case HashType.SHA512:
                    using (var alg = SHA512.Create())
                        return alg.ComputeHash(data);
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        /// <summary>
        /// Compute hash of a file (streamed) to support large files.
        /// Returns hex (default) or base64 if asBase64 == true.
        /// </summary>
        public static string ComputeFileHash(string filePath, HashType type = HashType.SHA256, bool asBase64 = false, int bufferSize = 81920)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found", filePath);

            byte[] hash;
            using (FileStream fs = File.OpenRead(filePath))
            {
                switch (type)
                {
                    case HashType.MD5:
                        using (var alg = MD5.Create())
                            hash = alg.ComputeHash(fs);
                        break;
                    case HashType.SHA1:
                        using (var alg = SHA1.Create())
                            hash = alg.ComputeHash(fs);
                        break;
                    case HashType.SHA256:
                        using (var alg = SHA256.Create())
                            hash = alg.ComputeHash(fs);
                        break;
                    case HashType.SHA384:
                        using (var alg = SHA384.Create())
                            hash = alg.ComputeHash(fs);
                        break;
                    case HashType.SHA512:
                        using (var alg = SHA512.Create())
                            hash = alg.ComputeHash(fs);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("type");
                }
            }

            return asBase64 ? Convert.ToBase64String(hash) : BytesToHex(hash);
        }

        /// <summary>
        /// Constant-time comparison to avoid timing attacks.
        /// </summary>
        public static bool AreEqual(byte[] a, byte[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        /// <summary>
        /// Verify that the provided plain text matches the expected hash string.
        /// expectedHash is hex by default; if expectedIsBase64==true then treat as base64.
        /// </summary>
        public static bool VerifyHash(string plainText, string expectedHash, HashType type = HashType.SHA256, Encoding encoding = null, bool expectedIsBase64 = false)
        {
            if (plainText == null) throw new ArgumentNullException("plainText");
            if (expectedHash == null) throw new ArgumentNullException("expectedHash");

            encoding = encoding ?? Encoding.UTF8;
            byte[] computed = ComputeHashBytes(encoding.GetBytes(plainText), type);
            byte[] expected;

            if (expectedIsBase64)
            {
                try { expected = Convert.FromBase64String(expectedHash); }
                catch { return false; }
            }
            else
            {
                try { expected = HexToBytes(expectedHash); }
                catch { return false; }
            }

            return AreEqual(computed, expected);
        }

        #endregion

        #region HMAC

        /// <summary>
        /// Compute HMAC for the given text using provided key. Result is hex (default) or base64 if asBase64==true.
        /// </summary>
        public static string ComputeHmac(string text, byte[] key, HashType type = HashType.SHA256, Encoding encoding = null, bool asBase64 = false)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (key == null) throw new ArgumentNullException("key");
            encoding = encoding ?? Encoding.UTF8;
            byte[] data = encoding.GetBytes(text);

            byte[] mac;
            switch (type)
            {
                case HashType.MD5:
                    using (var h = new HMACMD5(key)) mac = h.ComputeHash(data);
                    break;
                case HashType.SHA1:
                    using (var h = new HMACSHA1(key)) mac = h.ComputeHash(data);
                    break;
                case HashType.SHA256:
                    using (var h = new HMACSHA256(key)) mac = h.ComputeHash(data);
                    break;
                case HashType.SHA384:
                    using (var h = new HMACSHA384(key)) mac = h.ComputeHash(data);
                    break;
                case HashType.SHA512:
                    using (var h = new HMACSHA512(key)) mac = h.ComputeHash(data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }

            return asBase64 ? Convert.ToBase64String(mac) : BytesToHex(mac);
        }

        #endregion

        #region Password hashing (PBKDF2)

        // Format used to store password hashes:
        // PBKDF2$<iterations>$<salt(base64)>$<hash(base64)>
        private const string PasswordHashPrefix = "PBKDF2";

        /// <summary>
        /// Create a password hash using PBKDF2 (Rfc2898). Returns a formatted string that includes iterations and salt.
        /// Default: 100000 iterations, salt 16 bytes, hash 32 bytes.
        /// </summary>
        public static string CreatePasswordHash(string password, int iterations = 100000, int saltSize = 16, int hashSize = 32)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (iterations <= 0) throw new ArgumentOutOfRangeException("iterations");

            byte[] salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash;
            // Use Rfc2898DeriveBytes (PBKDF2). On older frameworks this uses HMAC-SHA1 internally.
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                hash = pbkdf2.GetBytes(hashSize);
            }

            string saltB64 = Convert.ToBase64String(salt);
            string hashB64 = Convert.ToBase64String(hash);

            string formatted = string.Format("{0}${1}${2}${3}", PasswordHashPrefix, iterations, saltB64, hashB64);
            return formatted;
        }

        /// <summary>
        /// Verify a password against a stored PBKDF2 password hash (the format produced by CreatePasswordHash).
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrEmpty(storedHash)) throw new ArgumentNullException("storedHash");

            // Expected format: PBKDF2$iterations$saltB64$hashB64
            try
            {
                string[] parts = storedHash.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) return false;
                if (!string.Equals(parts[0], PasswordHashPrefix, StringComparison.OrdinalIgnoreCase)) return false;

                int iterations = int.Parse(parts[1]);
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expectedHash = Convert.FromBase64String(parts[3]);

                byte[] computed;
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    computed = pbkdf2.GetBytes(expectedHash.Length);
                }

                return AreEqual(computed, expectedHash);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Helpers (hex / base64 conversions)

        /// <summary>
        /// Convert byte array to hex string (lowercase).
        /// </summary>
        public static string BytesToHex(byte[] data)
        {
            if (data == null) return null;
            var sb = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        /// Convert hex string to byte array.
        /// </summary>
        public static byte[] HexToBytes(string hex)
        {
            if (hex == null) return null;
            if (hex.Length % 2 != 0) throw new FormatException("Invalid hex string length");

            int l = hex.Length / 2;
            byte[] result = new byte[l];
            for (int i = 0; i < l; i++)
                result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return result;
        }

        #endregion
    }
}
