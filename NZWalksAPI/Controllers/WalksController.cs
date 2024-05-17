using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
      
        //Injection of the new repository
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        //Retrieve all walks from DB
        //GET: https://localhost:44325/api/walks
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
                [FromQuery] string? filterOn, 
                [FromQuery] string? filterQuery, 
                [FromQuery] string? sortBy, 
                [FromQuery] bool isAscending, 
                [FromQuery] int pageNumber,
                [FromQuery] int pageSize)
        {
            //Get the domain walks from the repository method
            List<Walk> walks = await walkRepository.GetAll(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize);

            //Return the DTO to the client
            return Ok(mapper.Map<List<WalkDto>>(walks));
        }


        //Retrieve walks by Id from DB
        //GET: https://localhost:44325/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //Get from repo
            var walk = await walkRepository.GetById(id);
            if (walk == null)
                return NotFound();

            //Return DTO to client
            return Ok(mapper.Map<WalkDto>(walk));
        }

        //Create walk
        //POST: https://localhost:44325/api/walks
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody]CreateWalkRequestDto walkDtoRq)
        {
            //convert DTO to Domain Model
            var walk = mapper.Map<Walk>(walkDtoRq);

            //create the walk 
            walk = await walkRepository.Create(walk);

            //Map the Domain walk to DTO 
            var walkDto = mapper.Map<WalkDto>(walk);

            //First parameter is used to add a header Location that contains the url to access this created object
            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

        //Update walk by id
        //PUT: https://localhost:44325/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateWalkRequestDto walkDtoRq)
        {
            var walk = mapper.Map<Walk>(walkDtoRq);

            //updare with repo
            walk = await walkRepository.Update(id, walk);

            if (walk == null)
                return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }

        //Delete walk by id
        //DELETE: https://localhost:44325/api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            //Delete with repo
            var walk = await walkRepository.Delete(id);
            if (walk == null)
                return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }
    }
}
