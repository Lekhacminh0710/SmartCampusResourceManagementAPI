using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserAccount user);
    }
}
