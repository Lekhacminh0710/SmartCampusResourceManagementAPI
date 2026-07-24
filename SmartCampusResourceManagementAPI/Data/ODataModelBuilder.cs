using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Data
{
    public static class ODataModelBuilderExtensions
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<LearningResource>("LearningResources");
            builder.EntityType<LearningResource>().HasKey(r => r.ResourceId);

            builder.EntitySet<ResourceCategory>("Categories");
            builder.EntityType<ResourceCategory>().HasKey(c => c.CategoryId);

            builder.EntitySet<UserAccount>("Users");
            builder.EntityType<UserAccount>().HasKey(u => u.UserId);

            return builder.GetEdmModel();
        }
    }
}
