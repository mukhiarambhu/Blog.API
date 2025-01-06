using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.Dto;
using Blog.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        public BlogController(IBlogPostRepository blogPostRepository, IMapper mapper,ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogs([FromBody] AddBlogRequestDto addBlogRequestDto)
        {
            if (ModelState.IsValid)
            {
                var blogPost = new BlogPost()
                {
                    Title = addBlogRequestDto.Title,
                    ShortDescription = addBlogRequestDto.ShortDescription,
                    Content = addBlogRequestDto.Content,
                    FeaturedImageUrl = addBlogRequestDto.FeaturedImageUrl,
                    UrlHandle = addBlogRequestDto.UrlHandle,
                    PublishedDate = addBlogRequestDto.PublishedDate,
                    Author = addBlogRequestDto.Author,
                    isVisible = addBlogRequestDto.isVisible,
                    Categories = new List<Category>()
                };

                foreach ( var categoryGuid in addBlogRequestDto.Categories)
                {
                    var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                    if(existingCategory is not null)
                    {
                        blogPost.Categories.Add(existingCategory);
                    }

                }

                blogPost = await blogPostRepository.CreateAsync(blogPost);

                var blogPostDto = mapper.Map<BlogPostDto>(blogPost);
   
                return Ok(blogPostDto);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await blogPostRepository.GetAllAsync();
            if (blogs == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<IEnumerable<BlogPostDto>>(blogs));
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var blog = await blogPostRepository.GetByIdAsync(id);
            if (blog == null) {
                return NotFound();
            }
            return Ok(mapper.Map<BlogPostDto>(blog));
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateBlogRequest updateBlogRequest)
        {
            var blogEntity = new BlogPost
            {
                Title = updateBlogRequest.Title,
                ShortDescription = updateBlogRequest.ShortDescription,
                Content = updateBlogRequest.Content,
                FeaturedImageUrl = updateBlogRequest.FeaturedImageUrl,
                UrlHandle = updateBlogRequest.UrlHandle,
                PublishedDate = updateBlogRequest.PublishedDate,
                Author=updateBlogRequest.Author,
                isVisible=updateBlogRequest.isVisible,
                Categories=new List<Category>()

            };

            foreach (var categoryGuid in updateBlogRequest.categories) {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory != null) {
                    blogEntity.Categories.Add(existingCategory);
                }
            
            }
            blogEntity = await blogPostRepository.UpdateAsync(id, blogEntity);
            if (blogEntity == null) {
                return NotFound();
            }
            return Ok(mapper.Map<BlogPostDto>(blogEntity));
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Deletecategory([FromRoute] Guid id)
        {
            var blog = await blogPostRepository.DeleteAsync(id);
            if (blog == null) {
                return NotFound();
            }
            return Ok(mapper.Map<BlogPostDto>(blog));
        }
        [HttpGet]
        [Route("{urlHandle}")]

        public async Task<IActionResult> GetBlogByUrlHandle([FromRoute] string urlHandle)
        {
            var blog = await blogPostRepository.GetByUrlhandleAsync(urlHandle);
            if (blog == null) {

                return NotFound();
            }

            return Ok(mapper.Map<BlogPostDto> (blog));

        }
    }
}
