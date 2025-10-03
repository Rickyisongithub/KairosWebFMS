using AutoMapper;
using KairosWebAPI.Models.Dto;
using KairosWebAPI.Models.Entities;

namespace KairosWebAPI.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Journey, JourneyDto>().ReverseMap();
            CreateMap<JourneyDetail, JourneyDetailDto>().ReverseMap();
        }
    }
}
