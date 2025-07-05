using System.IO;
using System.Security.Cryptography;

namespace Finance_Manager_WPF_Front;

public static class Decryptor
{
    public static Stream DecryptWithOpenSsl(Stream encrypted, string password)
    {
        using var msInput = new MemoryStream();
        encrypted.CopyTo(msInput);
        var data = msInput.ToArray();

        var salt = data[8..16];
        var encryptedData = data[16..];

        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        using var aes = Aes.Create();
        aes.Key = deriveBytes.GetBytes(32);
        aes.IV = deriveBytes.GetBytes(16);

        using var decryptor = aes.CreateDecryptor();
        using var msEncrypted = new MemoryStream(encryptedData);
        using var cryptoStream = new CryptoStream(msEncrypted, decryptor, CryptoStreamMode.Read);
        var output = new MemoryStream();
        cryptoStream.CopyTo(output);
        output.Seek(0, SeekOrigin.Begin);
        return output;
    }
}