using Blogedium_api.Data;
using Blogedium_api.Interfaces.Repository;
using Blogedium_api.Modals;

namespace Blogedium_api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CommentModal> CreateComment(CommentModal commentModal)
        {
            await _context.Comments.AddAsync(commentModal);
            await _context.SaveChangesAsync();
            return commentModal;
        }
        public async Task<CommentModal?> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id); /// if 
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            return comment;
        }
        public async Task<CommentModal?> UpdateComment(int id, CommentModal commentModal)
        {
            var comment = await _context.Comments.FindAsync(id); // not
            if ( comment != null)
            {
                comment.FirstName = commentModal.FirstName;
                comment.LastName = commentModal.LastName;
                comment.CommentContent = commentModal.CommentContent;
                await _context.SaveChangesAsync();
            }
            return comment;
        }
        public async Task<CommentModal?> FindComment (int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            return comment;
        }

    }
}