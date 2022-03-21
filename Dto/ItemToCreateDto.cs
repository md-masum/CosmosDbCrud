using CosmosDbCrud.MapperProfile;
using CosmosDbCrud.Models;
using Newtonsoft.Json;

namespace CosmosDbCrud.Dto
{
    public class ItemToCreateDto : IMapFrom<Item>
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }
    }
}
