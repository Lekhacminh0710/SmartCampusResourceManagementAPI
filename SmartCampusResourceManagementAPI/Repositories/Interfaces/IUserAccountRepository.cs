using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Repositories.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<UserAccount?> GetByEmailAsync(string email);
    }
}
