using Microsoft.AspNetCore.Mvc;

namespace TD1.Models.Repository
{
    public interface IDataRepository<TEntity>
    {

        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<IEnumerable<TEntity>>> GetAllByNameAsync(string name);

        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByNameAsync(string name);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
