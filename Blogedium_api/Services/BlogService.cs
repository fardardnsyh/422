using Blogedium_api.Exceptions;
using Blogedium_api.Interfaces.Repository;
using Blogedium_api.Interfaces.Services;
using Blogedium_api.Modals;

namespace Blogedium_api.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogModal> CreateBlogAsync(BlogModal blogModal)
        {
            if (string.IsNullOrWhiteSpace(blogModal.Image))
            {
                throw new ArgumentException("Please upload an image");
            }
            if (string.IsNullOrWhiteSpace(blogModal.Title))
            {
                throw new ArgumentException("Please enter the title");
            }
            if (string.IsNullOrWhiteSpace(blogModal.Content))
            {
                throw new ArgumentException("Please enter the content");
            }
            return await _blogRepository.CreateBlog(blogModal);
        }

        public async Task<BlogModal> DeleteBlogAsync(int id)
        {
            var blogs = await _blogRepository.FindById(id);
            if (blogs != null)
            {
                return await _blogRepository.DeleteBlog(id);
            }
            throw new NotFoundException($"Blog with ID'{id}' not found");
        }
        
        public async Task<IEnumerable<BlogModal>> GetAllAsync()
        {
            return await _blogRepository.GetAll();
        }

        public async Task<BlogModal?> GetBlogAsync(int id)
        {
            var blog = await _blogRepository.FindById(id); //not null
            if (blog != null)
            {
                return await _blogRepository.GetBlog(id);
            } 
            throw new NotFoundException($"Blog with '{id}' not found");
        }

        public async Task<BlogModal> UpdateBlogAsync(int id, BlogModal blogModal)
        {
            var blog = await _blogRepository.FindById(id);
            if (blog != null)
            {
                return await _blogRepository.UpdateBlog(id, blogModal);
            }
            throw new NotFoundException($"Blog with '{id}' not found");
        }

        public async Task<BlogModal?> IncrementReadCountAsync(int id)
        {
            return await _blogRepository.IncrementReadCount(id);
        }
    }
}