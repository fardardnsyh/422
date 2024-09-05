using Blogedium_api.Modals;

namespace Blogedium_api.Interfaces.Repository
{
    public interface IBlogRepository
    {
        Task<BlogModal> CreateBlog (BlogModal blogModal);
        Task<IEnumerable<BlogModal>> GetAll ();
        Task<BlogModal?> GetBlog (int id);
        Task<BlogModal?> DeleteBlog (int id); 
        Task<BlogModal?> UpdateBlog (int id, BlogModal blogModal);
        Task<BlogModal?> FindById (int id);
        Task<BlogModal?> IncrementReadCount (int id);
    }
}