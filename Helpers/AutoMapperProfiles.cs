using AutoMapper;
using CityGuide.Dtos;
using CityGuide.Models;

namespace CityGuide.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            CreateMap<City, CityForListDto>().ForMember(dest => dest.PhotoUrl, opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); });
            CreateMap<City, CityForDetailDto>();
            CreateMap<Photo, PhotoForCreationDto>();
            CreateMap<PhotoForReturnDto, Photo>();
        }
    }
}
