using System;
using System.Security.Cryptography;
using System.Text;

namespace SecureWebhook
{
    public class SecureWebhookHelper
    {
        private readonly byte[] _encryptionKey;
        private readonly byte[] _hmacKey;

        public SecureWebhookHelper(string base64EncryptionKey, string base64HmacKey)
        {
            _encryptionKey = Convert.FromBase64String(base64EncryptionKey);
            _hmacKey = Convert.FromBase64String(base64HmacKey);

            if (_encryptionKey.Length != 32)
                throw new ArgumentException("Encryption key must be 32 bytes (base64-encoded).");
            if (_hmacKey.Length != 32)
                throw new ArgumentException("HMAC key must be 32 bytes (base64-encoded).");
        }

        public bool VerifySignature(string base64Payload, string expectedSignature)
        {
            using (var hmac = new HMACSHA256(_hmacKey))
            {
                var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(base64Payload));
                var computedHex = BitConverter.ToString(computed).Replace("-", "").ToLowerInvariant();
                return computedHex == expectedSignature;
            }
        }

        public string Decrypt(string base64Payload, string base64IV)
        {
            var encryptedData = Convert.FromBase64String(base64Payload);
            var iv = Convert.FromBase64String(base64IV);

            using (var aes = Aes.Create())
            {
                aes.Key = _encryptionKey;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor())
                {
                    var decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }

        public bool TryDecryptAndVerify(string base64Payload, string base64IV, string signature, out string decryptedJson)
        {
            decryptedJson = string.Empty;
            if (!VerifySignature(base64Payload, signature))
                return false;

            try
            {
                decryptedJson = Decrypt(base64Payload, base64IV);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
