using System.ComponentModel.DataAnnotations;

namespace SmartCampusResourceManagementAPI.DTOs
{
    public class ResourceDto
    {
        public int ResourceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string ContentUrl { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public string? Source { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int CreatedById { get; set; }
        public string? CreatedByName { get; set; }
    }

    public class CreateResourceDto
    {
        [Required]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "Title phai tu 5 den 120 ky tu.")]
        public string Title { get; set; } = string.Empty;

        public string? Summary { get; set; }

        [Required]
        [Url(ErrorMessage = "ContentUrl phai dung dinh dang URL.")]
        public string ContentUrl { get; set; } = string.Empty;

        [NotInFuture]
        public DateTime PublishedDate { get; set; }

        public string? Source { get; set; }

        [Range(0, 1, ErrorMessage = "Status chi nhan gia tri 0 hoac 1.")]
        public int Status { get; set; }

        public int CategoryId { get; set; }
    }

    public class UpdateResourceDto
    {
        [Required]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "Title phai tu 5 den 120 ky tu.")]
        public string Title { get; set; } = string.Empty;

        public string? Summary { get; set; }

        [Required]
        [Url(ErrorMessage = "ContentUrl phai dung dinh dang URL.")]
        public string ContentUrl { get; set; } = string.Empty;

        [NotInFuture]
        public DateTime PublishedDate { get; set; }

        public string? Source { get; set; }

        [Range(0, 1, ErrorMessage = "Status chi nhan gia tri 0 hoac 1.")]
        public int Status { get; set; }

        public int CategoryId { get; set; }
    }
}
