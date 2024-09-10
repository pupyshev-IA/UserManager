using Abdt.Loyal.UserManager.Domain;

namespace Abdt.Loyal.UserManager.BusinessLogic.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
