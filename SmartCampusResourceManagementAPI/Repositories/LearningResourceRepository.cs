using Microsoft.EntityFrameworkCore;
using SmartCampusResourceManagementAPI.Data;
using SmartCampusResourceManagementAPI.Models;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;

namespace SmartCampusResourceManagementAPI.Repositories
{
    public class LearningResourceRepository : ILearningResourceRepository
    {
        private readonly AppDbContext _context;

        public LearningResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LearningResource>> GetAllAsync()
        {
            return await _context.Resources
                .Include(r => r.Category)
                .Include(r => r.CreatedBy)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<LearningResource>> GetPublicAsync()
        {
            return await _context.Resources
                .Include(r => r.Category)
                .Include(r => r.CreatedBy)
                .AsNoTracking()
                .Where(r => r.Status == ResourceStatus.Active)
                .ToListAsync();
        }

        public async Task<LearningResource?> GetByIdAsync(int id)
        {
            return await _context.Resources
                .Include(r => r.Category)
                .Include(r => r.CreatedBy)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ResourceId == id);
        }

        public async Task<LearningResource> CreateAsync(LearningResource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<bool> UpdateAsync(int id, LearningResource resource)
        {
            var existing = await _context.Resources.FirstOrDefaultAsync(r => r.ResourceId == id);
            if (existing is null)
            {
                return false;
            }

            existing.Title = resource.Title;
            existing.Summary = resource.Summary;
            existing.ContentUrl = resource.ContentUrl;
            existing.PublishedDate = resource.PublishedDate;
            existing.Source = resource.Source;
            existing.Status = resource.Status;
            existing.CategoryId = resource.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Resources.FirstOrDefaultAsync(r => r.ResourceId == id);
            if (existing is null)
            {
                return false;
            }

            _context.Resources.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}
