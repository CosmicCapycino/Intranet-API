using System.Security.Cryptography;

namespace Intranet_API.Services
{
    public class PasswordHasher
    {
        private const int saltSize = 16;
        private const int hashSize = 32;
        private const int iterations = 1000000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, hashSize);

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] storedPassword = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] attemptHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, hashSize);

            return storedPassword.SequenceEqual(attemptHash);
        }
    }
}
