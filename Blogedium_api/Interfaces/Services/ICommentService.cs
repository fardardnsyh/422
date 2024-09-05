using Blogedium_api.Modals;
using System.Threading.Tasks;

namespace Blogedium_api.Interfaces.Services
{
    public interface ICommentService
    {
        Task<CommentModal> CreateCommentAsync (int blogId, CommentModal commentModal);
        Task<CommentModal?> DeleteCommentAsync (int id);
        Task<CommentModal?> UpdateCommentAsync (int id, CommentModal commentModal);
        Task<CommentModal?> GetCommentByIDAsync (int id);
    }
}