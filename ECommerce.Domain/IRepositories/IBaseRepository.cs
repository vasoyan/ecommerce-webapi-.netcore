using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerce.Domain.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Returns the list of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets the entity by it's identifier.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Inserts the entity.
        /// </summary>
        /// <param name="entity">The entity</param>
        Task<T> SaveAsync(T entity);

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="entity"></param>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Makes soft deletion the entity by it's identifier. Sets it's status to inactive. Does not delete the entity from database.
        /// </summary>
        /// <param name="id">Identifier</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="expression">An expression to check for being empty.</param>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="expression">An expression to get the element.</param>
        /// <returns>Null if element is not found; otherwise, the first element in source.</returns>
        Task<T> GetDefaultAsync(Expression<Func<T, bool>> expression);

        Task<T> GetSingleDefaultAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Returns the list of the elements of a sequence.
        /// </summary>
        /// <param name="expression">An expression to get the list of the elements.</param>
        /// <returns>Returns the list of the elements of a sequence.</returns>
        Task<List<T>> GetDefaultsAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Gets the first element according to the parameters given.
        /// </summary>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="select"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <returns>Returns null if the element is not found; otherwise, the first element in source.</returns>
        Task<TResult?> GetFilteredFirstOrDefaultAsync<TResult>(
            Expression<Func<T, TResult>> select, //select
            Expression<Func<T, bool>> where, //where
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, //orderBy
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);//Join  

        /// <summary>
        /// Gets all elements according to the parameters given.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="select"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <returns>Returns all elements according to the parameters given.</returns>
        Task<List<TResult>?> GetFilteredListAsync<TResult>(
            Expression<Func<T, TResult>> select, //select
            Expression<Func<T, bool>> where, //where
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, //orderBy
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);//Join

        /// <summary>
        /// Gets all elements according to the parameters given.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="select"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Returns all elements according to the parameters given with Pagination(page index and number of rows per page).</returns>
        Task<IEnumerable<TResult>?> GetFilteredPagedListAsync<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int pageIndex = 1,
            int pageSize = 20);
    }
}
