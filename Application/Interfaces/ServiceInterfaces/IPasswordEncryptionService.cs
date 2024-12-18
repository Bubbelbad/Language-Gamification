
namespace Application.Interfaces.ServiceInterfaces
{
    public interface IPasswordEncryptionService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
