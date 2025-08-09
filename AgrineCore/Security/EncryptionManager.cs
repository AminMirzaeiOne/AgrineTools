using System;
using System.Security.Cryptography;
using System.Text;

namespace AgrineCore.Security
{
    /// <summary>
    /// Basic cryptographic helpers: AES (CBC), RSA, random bytes, RSA key generation (XML)
    /// </summary>
    public static class EncryptionManager
    {
        /// <summary>
        /// Generate cryptographically-secure random bytes.
        /// </summary>
        public static byte[] GenerateRandomBytes(int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException("length");
            byte[] data = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }
            return data;
        }

        /// <summary>
        /// Generate AES-256 key (32 bytes).
        /// </summary>
        public static byte[] GenerateAesKey()
        {
            return GenerateRandomBytes(32);
        }

        /// <summary>
        /// Generate AES IV (16 bytes).
        /// </summary>
        public static byte[] GenerateIv()
        {
            return GenerateRandomBytes(16);
        }

        /// <summary>
        /// AES-CBC encrypt (bytes). Returns ciphertext bytes.
        /// </summary>
        public static byte[] AesEncrypt(byte[] plain, byte[] key, byte[] iv)
        {
            if (plain == null) throw new ArgumentNullException("plain");
            if (key == null) throw new ArgumentNullException("key");
            if (iv == null) throw new ArgumentNullException("iv");

            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(plain, 0, plain.Length);
                }
            }
        }

        /// <summary>
        /// AES-CBC decrypt (bytes). Returns plaintext bytes.
        /// </summary>
        public static byte[] AesDecrypt(byte[] cipher, byte[] key, byte[] iv)
        {
            if (cipher == null) throw new ArgumentNullException("cipher");
            if (key == null) throw new ArgumentNullException("key");
            if (iv == null) throw new ArgumentNullException("iv");

            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
                }
            }
        }

        #region RSA helpers (using RSACryptoServiceProvider for broad compatibility)

        /// <summary>
        /// Generate RSA key pair and return XML strings (publicXml, privateXml).
        /// Note: ToXmlString/FromXmlString are available in .NET Framework / .NET Core (compat).
        /// </summary>
        public static void GenerateRsaKeyPair(int keySize, out string publicKeyXml, out string privateKeyXml)
        {
            if (keySize < 1024) throw new ArgumentOutOfRangeException("keySize");

            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                try
                {
                    privateKeyXml = rsa.ToXmlString(true);  // private + public
                    publicKeyXml = rsa.ToXmlString(false); // public only
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// RSA encrypt (OAEP). Accepts public key xml.
        /// </summary>
        public static byte[] RsaEncrypt(byte[] data, string publicKeyXml)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (string.IsNullOrEmpty(publicKeyXml)) throw new ArgumentNullException("publicKeyXml");

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.FromXmlString(publicKeyXml);
                    return rsa.Encrypt(data, true); // OAEP
                }
                finally
                {
                    // Cleanup
                }
            }
        }

        /// <summary>
        /// RSA decrypt (OAEP). Accepts private key xml.
        /// </summary>
        public static byte[] RsaDecrypt(byte[] encryptedData, string privateKeyXml)
        {
            if (encryptedData == null) throw new ArgumentNullException("encryptedData");
            if (string.IsNullOrEmpty(privateKeyXml)) throw new ArgumentNullException("privateKeyXml");

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.FromXmlString(privateKeyXml);
                    return rsa.Decrypt(encryptedData, true);
                }
                finally
                {
                }
            }
        }

        #endregion
    }
}
