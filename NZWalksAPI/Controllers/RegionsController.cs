using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
      
        //Injection of the new repository
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        //Retrieve all regions from DB
        //GET: https://localhost:44325/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get the domain regions from the repository method
            List<Region> regions = await regionRepository.GetAll();

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
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //Get from repo
            var region = await regionRepository.GetById(id);
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
        public async Task<IActionResult> Create([FromBody]CreateRegionRequestDto regionDtoRq)
        {
            //convert DTO to Domain Model
            var region = new Region
            {
                Code = regionDtoRq.Code,
                Name = regionDtoRq.Name,
                RegionImgUrl = regionDtoRq.RegionImgUrl
            };

            //create the region 
            region = await regionRepository.Create(region);

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

        //Update Region by id
        //PUT: https://localhost:44325/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto regionDtoRq)
        {
            var region = new Region
            {
                Code= regionDtoRq.Code,
                Name = regionDtoRq.Name,
                RegionImgUrl= regionDtoRq.RegionImgUrl
            };

            //updare with repo
            region = await regionRepository.Update(id, region);

            if (region == null)
                return NotFound();

            //Return DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImgUrl = region.RegionImgUrl
            };

            return Ok(regionDto);
        }

        //Delete Region by id
        //DELETE: https://localhost:44325/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            //Delete with repo
            var region = await regionRepository.Delete(id);
            if (region == null)
                return NotFound();

            //Return DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImgUrl = region.RegionImgUrl
            };

            return Ok(regionDto);
        }
    }
}
