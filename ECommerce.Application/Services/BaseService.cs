using AutoMapper;
using ECommerce.Application.IServices;
using ECommerce.Domain.Entities;
using ECommerce.Domain.IRepositories;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class BaseService<T, TDto, TVm> : IBaseService<T, TDto, TVm>
    where T : BaseEntity
    where TDto : class
    where TVm : class
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TVm>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TVm>>(entities);
        }

        public async Task<TVm> GetByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<TVm>(result);
        }

        public async Task<TVm> SaveAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            var result = await _repository.SaveAsync(entity);
            return _mapper.Map<TVm>(result);
        }

        public async Task<TVm> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<TVm>(result);
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await _repository.GetByIdAsync(id);
            await _repository.UpdateAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<TVm> GetDefault(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.GetDefaultAsync(expression);
            return _mapper.Map<TVm>(entity);
        }

        public async Task<TVm> GetSingleDefault(Expression<Func<T, bool>> expression)
        {
            var entity = await _repository.GetSingleDefaultAsync(expression);
            return _mapper.Map<TVm>(entity);
        }

        public async Task<IEnumerable<TVm>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            var entities = await _repository.GetDefaultsAsync(expression);
            return _mapper.Map<IEnumerable<TVm>>(entities);
        }
    }
}
