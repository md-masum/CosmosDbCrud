using CosmosDbCrud.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosDbCrud.Repository
{
    public class CosmosDbRepository<TEntity> : ICosmosDbRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Container _container;
        public CosmosDbRepository(
            CosmosClient cosmosDbClient)
        {
            var databaseName = CosmosClientInstance.DatabaseName;
            var containerName = CosmosClientInstance.ContainerName;
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            QueryDefinition query = new QueryDefinition(
                    "select * from c where c.type = @type ")
                .WithParameter("@type", typeof(TEntity).Name);
            var data = _container.GetItemQueryIterator<TEntity>(query);
            var results = new List<TEntity>();

            while (data.HasMoreResults)
            {
                var response = await data.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(QueryDefinition query)
        {
            var data = _container.GetItemQueryIterator<TEntity>(query);
            var results = new List<TEntity>();
            while (data.HasMoreResults)
            {
                var response = await data.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<TEntity> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<TEntity> AddAsync(TEntity item)
        {
            item.Id = Guid.NewGuid().ToString();
            item.Type = typeof(TEntity).Name;
            item.CreatedBy = null;
            item.CreatedDate = DateTime.UtcNow;
            var response = await _container.CreateItemAsync(item, new PartitionKey(item.Id));
            return response.Resource;
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity item)
        {
            item.Type = typeof(TEntity).Name;
            item.UpdateBy = null;
            item.UpdateDate = DateTime.UtcNow;
            var response = await _container.UpsertItemAsync(item, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Item>(id, new PartitionKey(id));
        }
    }
}
