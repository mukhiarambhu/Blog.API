using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Models.Dto;
using Blog.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repository
{
    public class BlogPostsRepository : IBlogPostRepository
    {
        private readonly BlogPostDbContext dbContext;

        public BlogPostsRepository(BlogPostDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var blog = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return null;
            }
            dbContext.BlogPosts.Remove(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);


        }

        public async Task<BlogPost> GetByUrlhandleAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.FirstOrDefaultAsync(x=>x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost> UpdateAsync(Guid id, BlogPost blogPost)
        {
            var blog = dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefault(x => x.Id == id);
            if (blog == null) {
                return null;
            }
            blog.Title = blogPost.Title;
            blog.ShortDescription = blogPost.ShortDescription;
            blog.Author = blogPost.Author;
            blog.PublishedDate = blogPost.PublishedDate;
            blog.isVisible = blogPost.isVisible;
            blog.Content = blogPost.Content;
            blog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            blog.UrlHandle = blogPost.UrlHandle;
            blog.Categories = blogPost.Categories;
           await dbContext.SaveChangesAsync();
            return blog;
        }
    }
}
