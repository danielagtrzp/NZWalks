using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            //this is able to map List as well and reversemap creates de inverse operation
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<CreateRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            
        }
    }
}
