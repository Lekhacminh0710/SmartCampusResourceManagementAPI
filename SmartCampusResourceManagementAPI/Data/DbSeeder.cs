using SmartCampusResourceManagementAPI.Models;
using SmartCampusResourceManagementAPI.Services;

namespace SmartCampusResourceManagementAPI.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var slide = new ResourceCategory { CategoryName = "Slide", Description = "Bai giang", IsActive = true };
            var lab = new ResourceCategory { CategoryName = "Lab", Description = "Bai thuc hanh", IsActive = true };
            var announcement = new ResourceCategory { CategoryName = "Announcement", Description = "Thong bao", IsActive = true };
            context.Categories.AddRange(slide, lab, announcement);

            var admin = new UserAccount
            {
                FullName = "System Admin",
                Email = "admin@campus.edu",
                Password = PasswordHasher.Hash("123456"),
                Role = UserRole.Admin,
                Department = Department.IT
            };
            var staff = new UserAccount
            {
                FullName = "Academic Staff",
                Email = "staff@campus.edu",
                Password = PasswordHasher.Hash("123456"),
                Role = UserRole.Staff,
                Department = Department.Academic
            };
            var itStaff = new UserAccount
            {
                FullName = "IT Staff",
                Email = "itstaff@campus.edu",
                Password = PasswordHasher.Hash("123456"),
                Role = UserRole.Staff,
                Department = Department.IT
            };
            context.Users.AddRange(admin, staff, itStaff);

            context.Resources.AddRange(
                new LearningResource
                {
                    Title = "Introduction to ASP.NET Core",
                    Summary = "Slide mo dau mon hoc Web API",
                    ContentUrl = "https://learn.microsoft.com/aspnet/core",
                    PublishedDate = DateTime.UtcNow.AddDays(-30),
                    Source = "Microsoft Learn",
                    Status = ResourceStatus.Active,
                    Category = slide,
                    CreatedBy = admin
                },
                new LearningResource
                {
                    Title = "Entity Framework Core Lab Exercise",
                    Summary = "Bai lab thuc hanh EF Core",
                    ContentUrl = "https://learn.microsoft.com/ef/core",
                    PublishedDate = DateTime.UtcNow.AddDays(-20),
                    Source = "Department of IT",
                    Status = ResourceStatus.Active,
                    Category = lab,
                    CreatedBy = itStaff
                },
                new LearningResource
                {
                    Title = "OData Query Options Overview",
                    Summary = "Slide gioi thieu OData",
                    ContentUrl = "https://www.odata.org/",
                    PublishedDate = DateTime.UtcNow.AddDays(-15),
                    Source = "OData.org",
                    Status = ResourceStatus.Active,
                    Category = slide,
                    CreatedBy = admin
                },
                new LearningResource
                {
                    Title = "JWT Authentication Lab",
                    Summary = "Bai lab thuc hanh JWT",
                    ContentUrl = "https://jwt.io/introduction",
                    PublishedDate = DateTime.UtcNow.AddDays(-10),
                    Source = "jwt.io",
                    Status = ResourceStatus.Active,
                    Category = lab,
                    CreatedBy = staff
                },
                new LearningResource
                {
                    Title = "Midterm Exam Schedule Announcement",
                    Summary = "Thong bao lich thi giua ky",
                    ContentUrl = "https://campus.edu/announcements/midterm",
                    PublishedDate = DateTime.UtcNow.AddDays(-5),
                    Source = "Academic Office",
                    Status = ResourceStatus.Active,
                    Category = announcement,
                    CreatedBy = staff
                },
                new LearningResource
                {
                    Title = "Draft: Upcoming Workshop",
                    Summary = "Ban nhap thong bao workshop",
                    ContentUrl = "https://campus.edu/announcements/workshop-draft",
                    PublishedDate = DateTime.UtcNow.AddDays(-2),
                    Source = "Academic Office",
                    Status = ResourceStatus.Inactive,
                    Category = announcement,
                    CreatedBy = staff
                }
            );

            context.SaveChanges();
        }
    }
}
