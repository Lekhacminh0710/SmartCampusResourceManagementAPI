using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;

namespace SmartCampusResourceManagementAPI.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ILearningResourceRepository _resourceRepository;
        private readonly IResourceCategoryRepository _categoryRepository;

        public ReportsController(ILearningResourceRepository resourceRepository, IResourceCategoryRepository categoryRepository)
        {
            _resourceRepository = resourceRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("it-only")]
        [Authorize(Policy = "ITDepartmentOnly")]
        public async Task<IActionResult> GetItReport()
        {
            var resources = await _resourceRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            return Ok(new
            {
                generatedAt = DateTime.UtcNow,
                totalResources = resources.Count(),
                activeResources = resources.Count(r => r.Status == Models.ResourceStatus.Active),
                totalCategories = categories.Count()
            });
        }
    }
}
