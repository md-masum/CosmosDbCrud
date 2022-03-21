using Newtonsoft.Json;

namespace CosmosDbCrud.Models
{
    public class Category : BaseEntity
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
