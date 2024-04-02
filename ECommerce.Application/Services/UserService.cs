using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ECommerce.Application.Services
{
    public class UserService(IUserRepository userRepository, ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IRefreshTokenService refreshTokenService,
        IOptions<JwtTokenSettings> jwtTokenSettings,
        IMapper mapper)
        : BaseService<User, UserDTO, UserVM>(userRepository, mapper), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IRefreshTokenService _refreshTokenService = refreshTokenService;
        private readonly JwtTokenSettings _jwtTokenSettings = jwtTokenSettings.Value;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserVM>?> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            // Define your filter, order by, and includes
            Expression<Func<User, bool>> filter = null;
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = q => q.OrderBy(p => p.Username);
            Func<IQueryable<User>, IIncludableQueryable<User, object>>? includes = null;

            var entities = await _userRepository.GetFilteredPagedListAsync(
                select: x => _mapper.Map<UserVM>(x),
                where: filter,
                orderBy: orderBy,
                include: includes,
                pageIndex,
                pageSize);

            return entities;
        }

        public async Task<UserVM?> GetUserByEmailAsync(string email)
        {
            Expression<Func<User, bool>> filter = u => u.Email.Trim() == email.Trim();
            Func<IQueryable<User>, IIncludableQueryable<User, object>> includes = q => q.Include(p => p.RefreshTokens).Include(p => p.Role);

            var userVM = await _userRepository.GetFilteredFirstOrDefaultAsync(
                select: x => _mapper.Map<UserVM>(x),
                where: filter,
                include: includes);

            return userVM;
        }

        public async Task<UserVM?> LoginAsync(LoginDTO loginDTO)
        {
            var userVM = await GetUserByEmailAsync(loginDTO.Email);

            if (userVM != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDTO.PasswordHash, userVM.PasswordHash);

                // Assuming you want to return null if the password is not valid
                if (isPasswordValid)
                {
                    userVM.JwtToken = _tokenService.GenerateAccessToken(userVM);
                    await UpdateAsync(_mapper.Map<UserDTO>(userVM));

                    RefreshTokenDTO refreshTokenDTO = new()
                    {
                        Token = _tokenService.GenerateRefreshToken(),
                        UserId = userVM.Id,
                        ExpiryDateTime = DateTime.UtcNow.AddMinutes(_jwtTokenSettings.RefreshTokenTTL),
                    };

                    // Generate refresh token
                    userVM.RefreshTokens.Add(await _refreshTokenService.SaveAsync(refreshTokenDTO));

                    return userVM;
                }
                else
                {
                    return null;
                }
            }

            return userVM;

        }

        public async Task<RefreshTokenVM?> RefreshTokenAsync(string token)
        {
            var refreshTokenData = await _refreshTokenService.GetFilteredFirstOrDefaultAsync(token);

            if (refreshTokenData != null)
            {
                UserVM userVM = await GetByIdAsync(refreshTokenData.UserId);
                ClaimsPrincipal claimsPrincipal = _tokenService.GetPrincipalFromExpiredToken(userVM.JwtToken);
                if (claimsPrincipal.Claims.FirstOrDefault().Subject.IsAuthenticated)
                {
                    userVM.JwtToken = _tokenService.GenerateAccessToken(userVM);
                    await UpdateAsync(_mapper.Map<UserDTO>(userVM));

                    RefreshTokenDTO refreshTokenDTO = new()
                    {
                        Token = _tokenService.GenerateRefreshToken(),
                        UserId = userVM.Id,
                        ExpiryDateTime = DateTime.UtcNow.AddMinutes(_jwtTokenSettings.RefreshTokenTTL),
                    };

                    // Generate refresh token
                    userVM.RefreshTokens.Add(await _refreshTokenService.SaveAsync(refreshTokenDTO));

                    refreshTokenData.User = userVM;
                    return refreshTokenData;
                }
            }

            return null;
        }

    }
}
