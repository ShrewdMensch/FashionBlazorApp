using AutoMapper;
using Domain;
using Application.DTOs;
namespace Application.AutoMapper.MappingProfiles
{
    public class AccessoryMappingProfile : Profile
    {
        public AccessoryMappingProfile()
        {
            CreateMap<Accessory, AccessoryDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id))
                .ForMember(destination => destination.Cost, option => option.MapFrom(p => p.Cost)).ReverseMap();
            
            CreateMap<TypeOfClothAccessoryDto, AccessoryWithQuantityDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Accessory))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.AccessoryId));
            
            CreateMap<Accessory, AccessoryWithQuantityDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id));
        }
    }
}
