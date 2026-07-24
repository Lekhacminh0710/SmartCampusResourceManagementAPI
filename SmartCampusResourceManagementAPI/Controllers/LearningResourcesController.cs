using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCampusResourceManagementAPI.DTOs;
using SmartCampusResourceManagementAPI.Models;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;

namespace SmartCampusResourceManagementAPI.Controllers
{
    [ApiController]
    [Route("api/resources")]
    public class LearningResourcesController : ControllerBase
    {
        private readonly ILearningResourceRepository _resourceRepository;
        private readonly IUserAccountRepository _userRepository;

        public LearningResourcesController(ILearningResourceRepository resourceRepository, IUserAccountRepository userRepository)
        {
            _resourceRepository = resourceRepository;
            _userRepository = userRepository;
        }

        private static ResourceDto ToDto(LearningResource resource) => new()
        {
            ResourceId = resource.ResourceId,
            Title = resource.Title,
            Summary = resource.Summary,
            ContentUrl = resource.ContentUrl,
            PublishedDate = resource.PublishedDate,
            Source = resource.Source,
            Status = (int)resource.Status,
            CategoryId = resource.CategoryId,
            CategoryName = resource.Category?.CategoryName,
            CreatedById = resource.CreatedById,
            CreatedByName = resource.CreatedBy?.FullName
        };

        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetPublic()
        {
            var resources = await _resourceRepository.GetPublicAsync();
            return Ok(resources.Select(ToDto));
        }

        [HttpGet("manage")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetManage()
        {
            var resources = await _resourceRepository.GetAllAsync();
            return Ok(resources.Select(ToDto));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ResourceDto>> GetById(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource is null)
            {
                return NotFound();
            }

            return Ok(ToDto(resource));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResourceDto>> Create([FromBody] CreateResourceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _resourceRepository.CategoryExistsAsync(dto.CategoryId))
            {
                ModelState.AddModelError(nameof(dto.CategoryId), "CategoryId khong ton tai.");
                return BadRequest(ModelState);
            }

            var email = User.FindFirstValue(ClaimTypes.Email);
            var currentUser = email is null ? null : await _userRepository.GetByEmailAsync(email);
            if (currentUser is null)
            {
                return Unauthorized();
            }

            var resource = new LearningResource
            {
                Title = dto.Title,
                Summary = dto.Summary,
                ContentUrl = dto.ContentUrl,
                PublishedDate = dto.PublishedDate,
                Source = dto.Source,
                Status = (ResourceStatus)dto.Status,
                CategoryId = dto.CategoryId,
                CreatedById = currentUser.UserId
            };

            var created = await _resourceRepository.CreateAsync(resource);
            return CreatedAtAction(nameof(GetById), new { id = created.ResourceId }, ToDto(created));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateResourceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _resourceRepository.CategoryExistsAsync(dto.CategoryId))
            {
                ModelState.AddModelError(nameof(dto.CategoryId), "CategoryId khong ton tai.");
                return BadRequest(ModelState);
            }

            var resource = new LearningResource
            {
                Title = dto.Title,
                Summary = dto.Summary,
                ContentUrl = dto.ContentUrl,
                PublishedDate = dto.PublishedDate,
                Source = dto.Source,
                Status = (ResourceStatus)dto.Status,
                CategoryId = dto.CategoryId
            };

            var updated = await _resourceRepository.UpdateAsync(id, resource);
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
            var deleted = await _resourceRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
