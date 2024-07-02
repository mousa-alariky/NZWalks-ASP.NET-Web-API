using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // region mapping
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
            
            // walks mapping
            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();

            // difficulty mapping
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
