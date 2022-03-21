using CosmosDbCrud.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosDbCrud.Repository
{
    public interface ICosmosDbRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync(QueryDefinition query);
        Task<TEntity> GetAsync(string id);
        Task<TEntity> AddAsync(TEntity item);
        Task<TEntity> UpdateAsync(string id, TEntity item);
        Task DeleteAsync(string id);
    }
}
