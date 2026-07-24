namespace SmartCampusResourceManagementAPI.Models
{
    public class LearningResource
    {
        public int ResourceId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Summary { get; set; }

        public string ContentUrl { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        public string? Source { get; set; }

        public ResourceStatus Status { get; set; }

        public int CategoryId { get; set; }
        public ResourceCategory? Category { get; set; }

        public int CreatedById { get; set; }
        public UserAccount? CreatedBy { get; set; }
    }
}
