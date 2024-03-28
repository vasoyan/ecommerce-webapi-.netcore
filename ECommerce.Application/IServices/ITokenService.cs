using ECommerce.Application.Models.VMs;
using System.Security.Claims;

namespace ECommerce.Application.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserVM userVM);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
