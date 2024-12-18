using Application.Interfaces.ServiceInterfaces;

namespace Application.Services.PasswordEncryption
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
