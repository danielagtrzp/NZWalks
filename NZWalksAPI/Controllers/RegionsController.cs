using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //Dependency inyection of the DBContext that I already set in the container in Program
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Retrieve all regions from DB
        //GET: https://localhost:44325/api/Regions
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            List<Region> regions = dbContext.Regions.ToList();
            return Ok(regions);
        }
    }
}
