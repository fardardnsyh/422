using Blogedium_api.Exceptions;
using Blogedium_api.Interfaces.Repository;
using Blogedium_api.Interfaces.Services;
using Blogedium_api.Modals;

namespace Blogedium_api.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogRepository _blogRepository;
        public CommentService(ICommentRepository commentRepository, IBlogRepository blogRepository)
        {
            _commentRepository = commentRepository;
            _blogRepository = blogRepository;
        }
        public async Task<CommentModal> CreateCommentAsync(int blogId, CommentModal commentModal)
        {
            var blog = await _blogRepository.FindById(blogId);
            if (blog == null)
            {
                throw new NotFoundException($"The Blog with ID '{blogId}' does not exist");
            }
            commentModal.BlogId = blogId;
            await _commentRepository.CreateComment(commentModal);
            return commentModal;
        }

        public async Task<CommentModal?> DeleteCommentAsync(int id)
        {
            var comment = await _commentRepository.FindComment(id); 
            if (comment != null)
            {   
                return await _commentRepository.DeleteComment(id);
            }
            throw new NotFoundException($"The comment '{id}' does not exist");
        }

        public async Task<CommentModal?> UpdateCommentAsync(int id, CommentModal commentModal)
        {
            var comment = await _commentRepository.FindComment(id);
            if (comment != null)
            {
                return await _commentRepository.UpdateComment(id, commentModal);
            }
            throw new NotFoundException($"The comment '{id}' does not exist to update");
        }

        public async Task<CommentModal?> GetCommentByIDAsync (int id)
        {
            var comment = await _commentRepository.FindComment(id);
            if (comment != null)
            {
                return comment;
            }
            throw new NotFoundException($"The comment '{id}' does not exist to retrieve");
        }
    }
}