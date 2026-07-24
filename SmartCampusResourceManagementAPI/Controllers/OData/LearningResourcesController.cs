using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using SmartCampusResourceManagementAPI.Data;
using SmartCampusResourceManagementAPI.Models;

namespace SmartCampusResourceManagementAPI.Controllers.OData
{
    [AllowAnonymous]
    public class LearningResourcesController : ODataController
    {
        private readonly AppDbContext _context;

        public LearningResourcesController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize = 100)]
        [HttpGet]
        public IQueryable<LearningResource> Get()
        {
            return _context.Resources.AsNoTracking();
        }
    }
}
