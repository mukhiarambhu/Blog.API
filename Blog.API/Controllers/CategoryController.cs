using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.Dto;
using Blog.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await categoryRepository.GetAllAsync();
            if (category == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<List<categoryDto>>(category));
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<categoryDto>(category));

        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequestDto addCategoryRequestDto)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<Category>(addCategoryRequestDto);
                category = await categoryRepository.AddAsync(category);

                var categoryDto = mapper.Map<categoryDto>(category);
                return Ok(categoryDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> updateEmployees(Guid id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {

            var category = mapper.Map<Category>(updateCategoryRequestDto);
            category = await categoryRepository.UpdateAsync(id, category);

            if (category == null) { 
             return NotFound();
            }
            return Ok(mapper.Map<categoryDto> (category));

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deleteEmployee([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<categoryDto>(category));
        }
    }
}
