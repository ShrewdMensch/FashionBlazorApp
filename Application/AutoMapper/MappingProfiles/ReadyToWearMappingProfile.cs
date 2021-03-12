using AutoMapper;
using Domain;
using Application.DTOs;
using System.Linq;
using Application.AutoMapper.ValueResolvers;

namespace Application.AutoMapper.MappingProfiles
{
    public class ReadyToWearMappingProfile : Profile
    {
        public ReadyToWearMappingProfile()
        {
            CreateMap<ReadyToWear, ReadyToWearDto>()
                .ForMember(destination => destination.TypeOfCloth, option => option.MapFrom(p => p.TypeOfCloth.Name))
                .ForMember(destination => destination.MainPhoto, option => option.MapFrom(p => p.Photos.FirstOrDefault().Url))
                .ForMember(destination => destination.PhotoUrls, option => option.MapFrom(p => p.Photos.Select(p => p.Url).ToList()))
                .ForMember(destination => destination.Cost, option =>
                {
                    option.PreCondition(r => r.TypeOfCloth != null);
                    option.MapFrom<ReadyToWearClothTotalCostResolver>();
                });

            CreateMap<Photo, PhotoDto>();
            CreateMap<ReadyToWearPhoto, ReadyToWearPhotoDto>().ReverseMap();
        }
    }
}
