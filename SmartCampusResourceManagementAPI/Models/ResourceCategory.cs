using System.ComponentModel.DataAnnotations;

namespace SmartCampusResourceManagementAPI.Models
{
    public class ResourceCategory
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<LearningResource> LearningResources { get; set; } = new List<LearningResource>();
    }
}
