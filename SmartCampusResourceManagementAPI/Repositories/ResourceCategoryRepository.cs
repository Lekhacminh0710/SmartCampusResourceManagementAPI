using Microsoft.EntityFrameworkCore;
using SmartCampusResourceManagementAPI.Data;
using SmartCampusResourceManagementAPI.Models;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;

namespace SmartCampusResourceManagementAPI.Repositories
{
    public class ResourceCategoryRepository : IResourceCategoryRepository
    {
        private readonly AppDbContext _context;

        public ResourceCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResourceCategory>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<ResourceCategory?> GetByIdAsync(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<ResourceCategory> CreateAsync(ResourceCategory category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(int id, ResourceCategory category)
        {
            var existing = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (existing is null)
            {
                return false;
            }

            existing.CategoryName = category.CategoryName;
            existing.Description = category.Description;
            existing.IsActive = category.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (existing is null)
            {
                return false;
            }

            _context.Categories.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasLearningResourcesAsync(int id)
        {
            return await _context.Resources.AnyAsync(r => r.CategoryId == id);
        }
    }
}
