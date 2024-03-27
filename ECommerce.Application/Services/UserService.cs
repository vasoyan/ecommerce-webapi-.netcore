using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Application.Models.DTOs;
using ECommerce.Application.Models.VMs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class UserService : BaseService<User, UserDTO, UserVM>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserVM>> GetFilteredPagedListAsync(int pageIndex = 1, int pageSize = 20)
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
    }
}
