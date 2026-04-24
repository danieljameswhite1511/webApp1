using System.Security.Cryptography;
using System.Text;

namespace Domain.Common.Cryptography;

public class Hashing
{
    public static string CreateHash(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.Create().ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool VerifyHash(string input, string hashedInput){
      return hashedInput.Equals(CreateHash(input));
    }
}