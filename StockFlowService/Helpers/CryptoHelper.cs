using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace StockFlowService.Helpers
{
    public static class CryptoHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("qwertyuiopasdfghjklzxcvbnm123456");
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("1234567890123456");

        public static string EncryptData(object data)
        {
            var plainText = JsonSerializer.Serialize(data);
            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToHexString(cipherBytes); // Return hex-encoded ciphertext
        }

        public static Dictionary<string, object> DecryptData(string encryptedHex)
        {
            if (string.IsNullOrWhiteSpace(encryptedHex))
                throw new ArgumentException("Input cannot be null or empty.");

            var cipherBytes = Convert.FromHexString(encryptedHex); // Correctly decode hex string

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.IV = IV;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            var json = Encoding.UTF8.GetString(plainBytes);

            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }
    }
}
