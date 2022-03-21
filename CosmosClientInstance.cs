using CosmosDbCrud.Repository;

namespace CosmosDbCrud
{
    public static class CosmosClientInstance
    {
        public static string DatabaseName = "TestDb";
        public static string ContainerName = "TestContainer";
        public static async Task InitializeCosmosClientAsync(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];
            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            services.AddSingleton(client);
            services.AddSingleton(typeof(ICosmosDbRepository<>), typeof(CosmosDbRepository<>));
        }
    }
}
