using System.ComponentModel.DataAnnotations;
using CosmosDbCrud.MapperProfile;
using CosmosDbCrud.Models;

namespace CosmosDbCrud.Dto
{
    public class PostDto : IMapFrom<Post>
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedOn { get; set; }
        [Required]
        public List<string> CategoryIds { get; set; }
        [Required]
        public List<string> TagIds { get; set; }
        public List<string> CommentIds { get; set; }
    }
}
