using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BloodDonation.Application.Utilities;

public static class StringUtilities
{
    public static string Hashing(this string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
    public static (string Password, byte[] Salt) HashPassword(this string password,
        byte[]? salt = null)
    {

        salt ??= RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return (hashed, salt);
    }

}
