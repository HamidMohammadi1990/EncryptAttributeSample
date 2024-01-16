using System.Text;
using System.Security.Cryptography;

namespace EncryptAttributeSample.Utilities;

public static class CryptographyUtility
{
    private const string GeneralHashKey = "D7CBD35D42D642DAA164207A";

    public static string Encrypt(this string value)
        => Encrypt(value, GeneralHashKey);

    public static string Decrypt(this string value)
        => Decrypt(value, GeneralHashKey);

    public static string Encrypt(this string value, string hashKey)
    {
        byte[] encrypted;
        using (var tdes = TripleDES.Create())
        {
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            var key = new UTF8Encoding().GetBytes(hashKey);
            var encryptor = tdes.CreateEncryptor(key, tdes.IV);
            using var ms = new MemoryStream();
            using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(value);
                cs.Write(plainBytes, 0, plainBytes.Length);
            }
            encrypted = ms.ToArray();
        }
        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(this string value, string hashKey)
    {
        byte[] decrypted;
        var key = new UTF8Encoding().GetBytes(hashKey);
        using (var tdes = TripleDES.Create())
        {
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            var decryptor = tdes.CreateDecryptor(key, tdes.IV);
            using var ms = new MemoryStream();
            using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Write))
            {
                var cipherBytes = Convert.FromBase64String(value);
                cs.Write(cipherBytes, 0, cipherBytes.Length);
            }
            decrypted = ms.ToArray();
        }
        var plainText = Encoding.UTF8.GetString(decrypted);
        return plainText;
    }
}