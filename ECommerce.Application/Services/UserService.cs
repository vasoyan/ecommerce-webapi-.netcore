using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ECommerce.Application.Services
{
    public class UserService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper) : BaseService<User, UserDTO, UserVM>(userRepository, mapper), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
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

            var userVM = await _userRepository.GetFilteredFirstOrDefaultAsync(
                select: x => _mapper.Map<UserVM>(x),
                where: filter);

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
                    var token = _tokenService.GenerateAccessToken(userVM);
                    
                    userVM.JwtToken = token;

                   var updateUser = _userRepository.UpdateAsync(_mapper.Map<User>(userVM));

                    return userVM;
                }
                else
                {
                    return null;
                }
            }

            return userVM;

        }

    }
}
