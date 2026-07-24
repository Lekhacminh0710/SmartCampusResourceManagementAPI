using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCampusResourceManagementAPI.DTOs;
using SmartCampusResourceManagementAPI.Models;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;

namespace SmartCampusResourceManagementAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class ResourceCategoriesController : ControllerBase
    {
        private readonly IResourceCategoryRepository _categoryRepository;

        public ResourceCategoriesController(IResourceCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private static CategoryDto ToDto(ResourceCategory category) => new()
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description,
            IsActive = category.IsActive
        };

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories.Select(ToDto));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return NotFound();
            }

            return Ok(ToDto(category));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new ResourceCategory
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var created = await _categoryRepository.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = created.CategoryId }, ToDto(created));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new ResourceCategory
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var updated = await _categoryRepository.UpdateAsync(id, category);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound();
            }

            if (await _categoryRepository.HasLearningResourcesAsync(id))
            {
                return BadRequest(new { message = "Khong the xoa Category dang co LearningResource su dung." });
            }

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
