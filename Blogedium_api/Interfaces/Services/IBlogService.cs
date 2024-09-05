using Blogedium_api.Modals;

namespace Blogedium_api.Interfaces.Services
{
    public interface IBlogService
    {
        Task<BlogModal> CreateBlogAsync (BlogModal blogModal);
        Task<IEnumerable<BlogModal>> GetAllAsync ();
        Task<BlogModal?> GetBlogAsync (int id);
        Task<BlogModal> DeleteBlogAsync (int id); 
        Task<BlogModal> UpdateBlogAsync (int id, BlogModal blogModal); 
        Task<BlogModal?> IncrementReadCountAsync (int id); 
        // IncrementReadCountAsync
    }
}