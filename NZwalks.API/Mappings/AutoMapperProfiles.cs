using AutoMapper;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Mappings
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
