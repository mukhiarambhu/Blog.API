using Blog.API.Models.Domain;

namespace Blog.API.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync( Guid id,Category category);
        Task<Category> DeleteAsync(Guid id);
    }
}
