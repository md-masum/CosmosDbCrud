using System.ComponentModel.DataAnnotations;
using CosmosDbCrud.MapperProfile;
using CosmosDbCrud.Models;

namespace CosmosDbCrud.Dto
{
    public class CategoryDto : IMapFrom<Category>
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
