using System.Text;
using System.Security.Cryptography;

namespace web_text_forum.Security
{
    public static class PasswordHasher
    {
        public static bool VerifyHashedPassword(string password, string hashedPassword)
        {            
            var hashString = GetBase64Hash_SHA256(password);
            
            return hashString == hashedPassword;
        }

        public static string GetBase64Hash_SHA256(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

            using var sha = SHA256.Create();

            // TODO: Use a salt for hashing passwords in production
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            return Convert.ToBase64String(hash);
        }
    }
}
