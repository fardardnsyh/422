using Blogedium_api.Modals;

namespace Blogedium_api.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task<CommentModal> CreateComment (CommentModal commentModal);
        Task<CommentModal?> DeleteComment (int id);
        Task<CommentModal?> UpdateComment (int id, CommentModal commentModal);
        Task<CommentModal?> FindComment (int id);
    }
}