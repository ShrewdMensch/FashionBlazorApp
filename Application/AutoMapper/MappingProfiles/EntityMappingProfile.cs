using AutoMapper;
using Domain;
using Application.DTOs;

namespace Application.AutoMapper.MappingProfiles
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Entity, EntityDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id));
            
            CreateMap<MeasurementHeader, EntityDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id)).ReverseMap();
            
            CreateMap<StandardSize, EntityDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id)).ReverseMap();

            CreateMap<Entity, Select2InputDto>()
                .ForMember(destination => destination.Text, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id));

            CreateMap<Photo, PhotoDto>();
        }
    }
}
