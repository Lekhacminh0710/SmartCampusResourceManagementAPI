using System.ComponentModel.DataAnnotations;

namespace SmartCampusResourceManagementAPI.Models
{
    public class UserAccount
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public Department Department { get; set; }

        public ICollection<LearningResource> CreatedResources { get; set; } = new List<LearningResource>();
    }
}
