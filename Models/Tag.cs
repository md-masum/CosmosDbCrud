using Newtonsoft.Json;

namespace CosmosDbCrud.Models
{
    public class Tag :BaseEntity
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
