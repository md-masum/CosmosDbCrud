using System.ComponentModel.DataAnnotations;
using CosmosDbCrud.MapperProfile;
using CosmosDbCrud.Models;

namespace CosmosDbCrud.Dto
{
    public class CommentDto : IMapFrom<Comment>
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
}
