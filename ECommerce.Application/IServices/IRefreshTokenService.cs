using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.IServices
{
    public interface IRefreshTokenService : IBaseService<RefreshToken, RefreshTokenDTO, RefreshTokenVM>
    {
        Task<RefreshTokenVM?> GetFilteredFirstOrDefaultAsync(string refreshToken);
    }
}
