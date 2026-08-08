using System.Security.Cryptography;
using System.Text;
using Domain.Common.GlobalConfig;

namespace Domain.Common.Cryptography;

public class Encryption
{
    private static byte[] GetKey()
    {
        var keyBytes = Encoding.UTF8.GetBytes(ApplicationConfig.SecurityKeys.SymmetricKey);
        
        // Ensure exact 32-byte key size for AES-256
        if (keyBytes.Length != 32)
        {
            throw new InvalidOperationException(
                $"SymmetricKey must be exactly 32 bytes (256 bits). Current length: {keyBytes.Length} bytes.");
        }
        
        return keyBytes;
    }

    public string Encrypt(string plaintext)
    {
        if (string.IsNullOrEmpty(plaintext)) return string.Empty;

        using var aes = Aes.Create();
        aes.Key = GetKey();
        aes.GenerateIV();

        using var memoryStream = new MemoryStream();
        // Write the 16-byte IV to the start of the stream
        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (var streamWriter = new StreamWriter(cryptoStream, Encoding.UTF8))
        {
            streamWriter.Write(plaintext);
            streamWriter.Flush();
            cryptoStream.FlushFinalBlock(); // Guarantees all PKCS7 padding & bytes are written
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return string.Empty;

        var fullCipher = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = GetKey();

        var ivSize = aes.BlockSize / 8; // 16 bytes
        if (fullCipher.Length < ivSize)
        {
            throw new ArgumentException("Invalid cipher text format.", nameof(cipherText));
        }

        var iv = new byte[ivSize];
        var cipher = new byte[fullCipher.Length - ivSize];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, ivSize);
        Buffer.BlockCopy(fullCipher, ivSize, cipher, 0, cipher.Length);

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(cipher);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream, Encoding.UTF8);

        return streamReader.ReadToEnd();
    }
}