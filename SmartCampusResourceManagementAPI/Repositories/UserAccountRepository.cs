using Microsoft.EntityFrameworkCore;
using SmartCampusResourceManagementAPI.Data;
using SmartCampusResourceManagementAPI.Models;
using SmartCampusResourceManagementAPI.Repositories.Interfaces;

namespace SmartCampusResourceManagementAPI.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly AppDbContext _context;

        public UserAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserAccount?> GetByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
