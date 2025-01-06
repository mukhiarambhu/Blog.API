using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repository
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly BlogPostDbContext dbContext;

        public CategoryRepository(BlogPostDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
           await dbContext.Categories.AddAsync(category);
            dbContext.SaveChanges();
            return category;
        }

        public async Task<Category> DeleteAsync(Guid id)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) { 
             return null;
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
           return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> UpdateAsync(Guid id, Category category)
        {
            var categoryEntity = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (categoryEntity == null) { 
             return null;
            }
            categoryEntity.Name = category.Name;
            categoryEntity.UrlHandle = category.UrlHandle;
            await dbContext.SaveChangesAsync();
            return categoryEntity;
        }
    }
}
