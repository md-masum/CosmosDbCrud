using AutoMapper;
using CosmosDbCrud.Dto;
using CosmosDbCrud.Models;
using CosmosDbCrud.Repository;
using Microsoft.Azure.Cosmos;

namespace CosmosDbCrud.Service
{
    public class BlogService : IBlogService
    {
        private readonly ICosmosDbRepository<Post> _postRepo;
        private readonly ICosmosDbRepository<Comment> _commentRepo;
        private readonly ICosmosDbRepository<Tag> _tagRepo;
        private readonly ICosmosDbRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public BlogService(ICosmosDbRepository<Post> postRepo,
            ICosmosDbRepository<Comment> commentRepo,
            ICosmosDbRepository<Tag> tagRepo,
            ICosmosDbRepository<Category> categoryRepo,
            IMapper mapper)
        {
            _postRepo = postRepo;
            _commentRepo = commentRepo;
            _tagRepo = tagRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        
        #region Tag Section

        public async Task<IList<TagDto>> GetAllTag()
        {
            var data = await _tagRepo.GetAllAsync();
            return _mapper.Map<IList<TagDto>>(data);
        }

        public async Task<TagDto> GetTagById(string id)
        {
            var data = await _tagRepo.GetAsync(id);
            return _mapper.Map<TagDto>(data);
        }

        public async Task<TagDto> CreateOrUpdateTag(TagDto tag)
        {
            if (string.IsNullOrWhiteSpace(tag.Id))
            {
                var response = await _tagRepo.AddAsync(_mapper.Map<Tag>(tag));
                return _mapper.Map<TagDto>(response);
            }

            var existingTag = await _tagRepo.GetAsync(tag.Id);
            if (existingTag is null) throw new Exception("No Tag found by provided Id");

            var data = _mapper.Map(tag, existingTag);
            var updateResponse = await _tagRepo.UpdateAsync(data.Id, data);
            return _mapper.Map<TagDto>(updateResponse);
        }

        public async Task DeleteTag(string id)
        {
            await _tagRepo.DeleteAsync(id);
        }

        #endregion

        #region Category Section

        public async Task<IList<CategoryDto>> GetAllCategory()
        {
            var data = await _categoryRepo.GetAllAsync();
            return _mapper.Map<IList<CategoryDto>>(data);
        }

        public async Task<CategoryDto> GetCategoryById(string id)
        {
            var data = await _categoryRepo.GetAsync(id);
            return _mapper.Map<CategoryDto>(data);
        }

        public async Task<CategoryDto> CreateOrUpdateCategory(CategoryDto category)
        {
            if (string.IsNullOrWhiteSpace(category.Id))
            {
                var response = await _categoryRepo.AddAsync(_mapper.Map<Category>(category));
                return _mapper.Map<CategoryDto>(response);
            }

            var existingTag = await _categoryRepo.GetAsync(category.Id);
            if (existingTag is null) throw new Exception("No Tag found by provided Id");

            var data = _mapper.Map(category, existingTag);
            var updateResponse = await _categoryRepo.UpdateAsync(data.Id, data);
            return _mapper.Map<CategoryDto>(updateResponse);
        }

        public async Task DeleteCategory(string id)
        {
            await _categoryRepo.DeleteAsync(id);
        }

        #endregion

        #region Comment Section

        public async Task<IList<CommentDto>> GetAllCommentByPostId(string postId)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                var allComment = await _commentRepo.GetAllAsync();
                return _mapper.Map<IList<CommentDto>>(allComment);
            }

            QueryDefinition query = new QueryDefinition(
                    "select * from c where c.type = @type and c.postId = @postId ")
                .WithParameter("@type", nameof(Comment))
                .WithParameter("@postId", postId);
            var data = await _commentRepo.GetAsync(query);

            return _mapper.Map<IList<CommentDto>>(data);
        }

        public async Task<CommentDto> GetCommentById(string id)
        {
            var data = await _commentRepo.GetAsync(id);
            return _mapper.Map<CommentDto>(data);
        }

        public async Task<CommentDto> CreateOrUpdateComment(CommentDto comment)
        {
            if (!await IsPostExist(comment.PostId)) throw new Exception("No Post found by provided id");

            if (string.IsNullOrWhiteSpace(comment.Id))
            {
                var response = await _commentRepo.AddAsync(_mapper.Map<Comment>(comment));
                await UpdatePostComment(response.PostId, response.Id);
                return _mapper.Map<CommentDto>(response);
            }

            var existingComment = await _commentRepo.GetAsync(comment.Id);
            if (existingComment is null) throw new Exception("No Comment found by provided Id");

            if (existingComment.PostId != comment.PostId) throw new Exception("You can't update post Id");

            var data = _mapper.Map(comment, existingComment);
            var updateResponse = await _commentRepo.UpdateAsync(data.Id, data);
            return _mapper.Map<CommentDto>(updateResponse);
        }

        private async Task<bool> IsPostExist(string postId)
        {
            var data = await _postRepo.GetAsync(postId);
            if (data is null) return false;
            return true;
        }
        private async Task UpdatePostComment(string postId, string commentId)
        {
            if (!await IsPostExist(postId)) throw new Exception("No Post found by provided id");

            var existingPost = await _postRepo.GetAsync(postId);
            if (existingPost.CommentIds is null)
            {
                existingPost.CommentIds = new List<string> {commentId};
            }
            else
            {
                existingPost.CommentIds.Add(commentId);
            }

            await _postRepo.UpdateAsync(postId, existingPost);
        }

        public async Task DeleteComment(string id)
        {
            await _commentRepo.DeleteAsync(id);
        }

        #endregion

        #region Post Section

        public async Task<IList<PostDto>> GetAllPost()
        {
            var data = await _postRepo.GetAllAsync();
            return _mapper.Map<IList<PostDto>>(data);
        }

        public async Task<PostDto> GetPostById(string id)
        {
            var data = await _postRepo.GetAsync(id);
            return _mapper.Map<PostDto>(data);
        }

        public async Task<PostDto> CreateOrUpdatePost(PostDto post)
        {
            if (string.IsNullOrWhiteSpace(post.Id))
            {
                var response = await _postRepo.AddAsync(_mapper.Map<Post>(post));
                return _mapper.Map<PostDto>(response);
            }

            var existingPost = await _postRepo.GetAsync(post.Id);
            if (existingPost is null) throw new Exception("No Post found by provided Id");

            var data = _mapper.Map(post, existingPost);
            var updateResponse = await _postRepo.UpdateAsync(data.Id, data);
            return _mapper.Map<PostDto>(updateResponse);
        }

        public async Task DeletePost(string id)
        {
            await _postRepo.DeleteAsync(id);
        }

        #endregion
    }
}
