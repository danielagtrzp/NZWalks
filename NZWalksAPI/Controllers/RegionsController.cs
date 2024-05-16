using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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
        public IActionResult GetAll()
        {
            //Get the domain regions from the db
            List<Region> regions = dbContext.Regions.ToList();

            //Map the domain to DTO regions
            List<RegionDto> regionsDto = new List<RegionDto>();
            foreach(Region region in regions)
            {
                regionsDto.Add(new RegionDto { 
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImgUrl = region.RegionImgUrl
                });
            }

            //Return the DTO to the client
            return Ok(regionsDto);
        }


        //Retrieve regions by Id from DB
        //GET: https://localhost:44325/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            //Get from db
            var region = dbContext.Regions.FirstOrDefault(i=>i.Id==id);
            if (region == null)
                return NotFound();

            //if exist Map to DTO
            var regionDto = new RegionDto{
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImgUrl = region.RegionImgUrl
            };

            //Return DTO to client
            return Ok(regionDto);
        }

        //Create region
        //POST: https://localhost:44325/api/Regions
        [HttpPost]
        public IActionResult Create([FromBody]CreateRegionRequestDto regionDtoRq)
        {
            //convert DTO to Domain Model
            var region = new Region
            {
                Code = regionDtoRq.Code,
                Name = regionDtoRq.Name,
                RegionImgUrl = regionDtoRq.RegionImgUrl
            };

            //Use Domain model to create the region 
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            //Map the Domain region to DTO 
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImgUrl = region.RegionImgUrl
            };

            //First parameter is used to add a header Location that contains the url to access this created object
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

    }
}
