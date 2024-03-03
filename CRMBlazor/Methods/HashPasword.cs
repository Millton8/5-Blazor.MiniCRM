using System.Security.Cryptography;
using System.Text;

namespace CRMBlazor.Methods
{
    public static class HashPassword
    {
        public static string HashPasword(string password)
        {
            const int keySize = 58;
            const int iterations = 10000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            var salt = new byte[58] { 157, 5, 188, 100, 208, 157, 90, 101, 236, 20, 31, 181, 38, 28, 62, 233, 127, 170, 20, 77, 211, 230, 171, 17, 181, 214, 90, 248, 239, 5, 90, 4, 66, 149, 175, 164, 39, 115, 203, 110, 71, 20, 206, 225, 77, 65, 30, 71, 150, 181, 129, 187, 66, 48, 223, 27, 255, 205 };
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
    }
}
