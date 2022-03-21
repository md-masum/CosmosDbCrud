using Newtonsoft.Json;

namespace CosmosDbCrud.Models
{
    public class Comment : BaseEntity
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        [JsonProperty(PropertyName = "postId")]
        public string PostId { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}
