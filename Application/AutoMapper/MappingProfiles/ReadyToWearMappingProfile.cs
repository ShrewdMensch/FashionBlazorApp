using AutoMapper;
using Domain;
using Application.DTOs;
using Application.AutoMapper.ValueResolvers;

namespace Application.AutoMapper.MappingProfiles
{
    public class ReadyToWearMappingProfile : Profile
    {
        public ReadyToWearMappingProfile()
        {
            CreateMap<ReadyToWear, ReadyToWearDto>()
                .ForMember(destination => destination.TypeOfCloth, option => option.MapFrom(p => p.TypeOfCloth.Name))
                .ForMember(destination => destination.Cost, option => option.MapFrom<ReadyToWearClothTotalCostResolver>());
        }
    }
}
