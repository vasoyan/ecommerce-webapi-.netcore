using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
        : BaseService<RefreshToken, RefreshTokenDTO, RefreshTokenVM>(refreshTokenRepository, mapper), IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<RefreshTokenVM?> GetFilteredFirstOrDefaultAsync(string refreshToken)
        {
            Expression<Func<RefreshToken, bool>> filter = u => u.Token.Trim() == refreshToken.Trim();
            Func<IQueryable<RefreshToken>, IIncludableQueryable<RefreshToken, object>> includes = q => q.Include(p => p.User);

            var userVM = await _refreshTokenRepository.GetFilteredFirstOrDefaultAsync(
                select: x => _mapper.Map<RefreshTokenVM>(x),
                where: filter,
                include: includes);

            return userVM;
        }

    }
}
