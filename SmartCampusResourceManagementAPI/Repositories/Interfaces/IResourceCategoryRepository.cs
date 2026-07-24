using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Repositories.Interfaces
{
    public interface IResourceCategoryRepository
    {
        Task<IEnumerable<ResourceCategory>> GetAllAsync();
        Task<ResourceCategory?> GetByIdAsync(int id);
        Task<ResourceCategory> CreateAsync(ResourceCategory category);
        Task<bool> UpdateAsync(int id, ResourceCategory category);
        Task<bool> DeleteAsync(int id);
        Task<bool> HasLearningResourcesAsync(int id);
    }
}
