using AzucareraPomalca.Core.Securities.Etities;

namespace AzucareraPomalca.Core.Securities.Services
{
    public interface ISecurityService
    {
        string HashPassword(string userName, string hashedPassword);
        bool VerifyHashedPassword(string userName, string hashedPassword, string providerPassword);

        SecurityEntity JwtSecurity(string jwtSecrectKey);
    }
}
