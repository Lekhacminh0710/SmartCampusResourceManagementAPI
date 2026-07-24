using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Repositories.Interfaces
{
    public interface ILearningResourceRepository
    {
        Task<IEnumerable<LearningResource>> GetAllAsync();
        Task<IEnumerable<LearningResource>> GetPublicAsync();
        Task<LearningResource?> GetByIdAsync(int id);
        Task<LearningResource> CreateAsync(LearningResource resource);
        Task<bool> UpdateAsync(int id, LearningResource resource);
        Task<bool> DeleteAsync(int id);
        Task<bool> CategoryExistsAsync(int categoryId);
    }
}
