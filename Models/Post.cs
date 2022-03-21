using Newtonsoft.Json;

namespace CosmosDbCrud.Models
{
    public class Post : BaseEntity
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "isPublished")]
        public bool IsPublished { get; set; }
        [JsonProperty(PropertyName = "publishedOn")]
        public DateTime PublishedOn { get; set; }

        [JsonProperty(PropertyName = "categoryIds")]
        public List<string> CategoryIds { get; set; }
        [JsonProperty(PropertyName = "tagIds")]
        public List<string> TagIds { get; set; }
        [JsonProperty(PropertyName = "commentIds")]
        public List<string> CommentIds { get; set; }
    }
}
