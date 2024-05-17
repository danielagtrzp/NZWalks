using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
      
        //Injection of the new repository
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //Retrieve all regions from DB
        //GET: https://localhost:44325/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get the domain regions from the repository method
            List<Region> regions = await regionRepository.GetAll();

            //Return the DTO to the client
            return Ok(mapper.Map<List<RegionDto>>(regions));
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

            //Return DTO to client
            return Ok(mapper.Map<RegionDto>(region));
        }

        //Create region
        //POST: https://localhost:44325/api/Regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]CreateRegionRequestDto regionDtoRq)
        {
            //convert DTO to Domain Model
            var region = mapper.Map<Region>(regionDtoRq);

                //create the region 
                region = await regionRepository.Create(region);

                //Map the Domain region to DTO 
                var regionDto = mapper.Map<RegionDto>(region);

                //First parameter is used to add a header Location that contains the url to access this created object
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update Region by id
        //PUT: https://localhost:44325/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto regionDtoRq)
        {
            var region = mapper.Map<Region>(regionDtoRq);

            //updare with repo
            region = await regionRepository.Update(id, region);

            if (region == null)
                return NotFound();

            return Ok(mapper.Map<RegionDto>(region));
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

            return Ok(mapper.Map<RegionDto>(region));
        }
    }
}
