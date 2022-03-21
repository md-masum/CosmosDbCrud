using CosmosDbCrud.Dto;

namespace CosmosDbCrud.Service
{
    public interface IBlogService
    {
        Task<IList<TagDto>> GetAllTag();
        Task<TagDto> GetTagById(string id);
        Task<TagDto> CreateOrUpdateTag(TagDto tag);
        Task DeleteTag(string id);

        Task<IList<CategoryDto>> GetAllCategory();
        Task<CategoryDto> GetCategoryById(string id);
        Task<CategoryDto> CreateOrUpdateCategory(CategoryDto category);
        Task DeleteCategory(string id);

        Task<IList<CommentDto>> GetAllCommentByPostId(string postId);
        Task<CommentDto> GetCommentById(string id);
        Task<CommentDto> CreateOrUpdateComment(CommentDto comment);
        Task DeleteComment(string id);

        Task<IList<PostDto>> GetAllPost();
        Task<PostDto> GetPostById(string id);
        Task<PostDto> CreateOrUpdatePost(PostDto post);
        Task DeletePost(string id);
    }
}
