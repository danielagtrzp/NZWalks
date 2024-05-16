using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //Retrieve all regions hardcoded
        //GET: https://localhost:44325/api/Regions
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            List<Region> regions = new List<Region>{
                new Region()
                {
                    Id= Guid.NewGuid(),
                    Code = "some",
                    Name = "RegionA",
                    RegionImgUrl = "https://someImage1"
                },
                new Region()
                {
                    Id= Guid.NewGuid(),
                    Code = "some2",
                    Name = "RegionB",
                    RegionImgUrl = "https://someImage2"
                }
            };

            return Ok(regions);
        }
    }
}
