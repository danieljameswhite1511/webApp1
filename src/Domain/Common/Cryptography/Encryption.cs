using System.Security.Cryptography;
using System.Text;
using Domain.Common.GlobalConfig;

namespace Domain.Common.Cryptography;

public static class Encryption
{
    public static string Encrypt(string plaintext) {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(ApplicationConfig.SecurityKeys.SymmetricKey);
        aes.GenerateIV();
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        memoryStream.Write(aes.IV, 0, aes.IV.Length);
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using var streamWriter = new StreamWriter(cryptoStream);
        streamWriter.Write(plaintext);
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string cypherText) {
        if (string.IsNullOrEmpty(cypherText)) return string.Empty;
        var fullCipher = Convert.FromBase64String(cypherText);
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(ApplicationConfig.SecurityKeys.SymmetricKey);
        var ivSize = aes.BlockSize / 8; 
        var iv = new byte[ivSize];
        var cipher = new byte[fullCipher.Length - ivSize];
        Buffer.BlockCopy(fullCipher, 0, iv, 0, ivSize);
        Buffer.BlockCopy(fullCipher, ivSize, cipher, 0, cipher.Length);
        aes.IV = iv;
        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(cipher);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}