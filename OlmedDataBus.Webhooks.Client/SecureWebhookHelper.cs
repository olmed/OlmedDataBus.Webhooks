using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SecureWebhook
{
    public class SecureWebhookHelper
    {
        private readonly byte[] _encryptionKey;
        private readonly byte[] _hmacKey;

        public SecureWebhookHelper(string encryptionKey, string hmacKey)
        {
            _encryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
            _hmacKey = Encoding.UTF8.GetBytes(hmacKey);

            if (_encryptionKey.Length != 32)
                throw new ArgumentException("Encryption key must be 32 bytes.");
            if (_hmacKey.Length != 32)
                throw new ArgumentException("HMAC key must be 32 bytes.");
        }

        private bool VerifySignature(string base64Payload, string expectedSignature)
        {
            using (var hmac = new HMACSHA256(_hmacKey))
            {
                var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(base64Payload));
                var computedHex = BitConverter.ToString(computed).Replace("-", "").ToLowerInvariant();
                return computedHex == expectedSignature;
            }
        }

        public bool TryDecryptAndVerifyWithIvPrefix(string guid, string webhookType, string base64PayloadWithIv, string signature, out string decryptedJson)
        {
            decryptedJson = string.Empty;

            try
            {
                var data = Convert.FromBase64String(base64PayloadWithIv);
                int ivLength = 16; // AES block size for CBC (128 bits = 16 bytes)
                if (data.Length <= ivLength)
                    return false;
                var iv = new byte[ivLength];
                Buffer.BlockCopy(data, 0, iv, 0, ivLength);
                var encryptedData = new byte[data.Length - ivLength];
                Buffer.BlockCopy(data, ivLength, encryptedData, 0, encryptedData.Length);

                using (var aes = Aes.Create())
                {
                    aes.Key = _encryptionKey;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        var decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                        decryptedJson = Encoding.UTF8.GetString(decryptedBytes);

                        // Przygotuj JSON dokładnie tak jak w PHP
                        var jsonData = $"{{\"guid\":\"{guid}\",\"webhookType\":\"{webhookType}\",\"webhookData\":{decryptedJson}}}";

                        // Weryfikacja podpisu HMAC
                        if (!VerifySignature(jsonData, signature))
                            return false;

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
