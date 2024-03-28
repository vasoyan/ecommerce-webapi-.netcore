using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Application.IServices
{
    public interface IBaseService<T, TDto, TVm>
        where T : BaseEntity
        where TDto : class
        where TVm : class
    {

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Returns all entities as a list.</returns>
        Task<IEnumerable<TVm>> GetAllAsync();

        /// <summary>
        /// Gets the entity by it's identifier.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        Task<TVm> GetByIdAsync(int id);

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        Task<TVm> SaveAsync(TDto dto);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity"></param>
        Task<TVm> UpdateAsync(TDto dto);

        /// <summary>
        /// Makes soft deletion the entity by it's identifier. Sets it's status to inactive. Does not delete the entity from database.
        /// </summary>
        /// <param name="id">Identifier</param>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="expression">An expression to check for being empty.</param>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<TVm> GetSingleDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="expression">An expression to get the element.</param>
        /// <returns>Null if element is not found; otherwise, the first element in source.</returns>
        Task<TVm> GetDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the list of the elements of a sequence.
        /// </summary>
        /// <param name="expression">An expression to get the list of the elements.</param>
        /// <returns>Returns the list of the elements of a sequence.</returns>
        Task<IEnumerable<TVm>> GetDefaults(Expression<Func<T, bool>> expression);

    }
}
