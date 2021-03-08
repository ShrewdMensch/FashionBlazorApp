using AutoMapper;
using Domain;
using Application.DTOs;

namespace Application.AutoMapper.MappingProfiles
{
    public class ClothRelatedMappingProfile : Profile
    {
        public ClothRelatedMappingProfile()
        {
            CreateMap<TypeOfClothMeasurementHeader, TypeOfClothMeasurementHeaderDto>()
                .ForMember(destination => destination.MeasurementHeader, option => option.MapFrom(p => p.MeasurementHeader.Name));
            
            CreateMap<TypeOfClothAccessory, TypeOfClothAccessoryDto>()
                .ForMember(destination => destination.Accessory, option => option.MapFrom(p => p.Accessory.Name));
            
            CreateMap<TypeOfClothIncurredExpense, TypeOfClothIncurredExpenseDto>()
                .ForMember(destination => destination.IncurredExpenseId, option => option.MapFrom(p => p.IncurredExpense.Id))
                .ForMember(destination => destination.IncurredExpenseName, option => option.MapFrom(p => p.IncurredExpense.Name))
                .ForMember(destination => destination.Cost, option => option.MapFrom(p => p.IncurredExpense.Cost));

            CreateMap<TypeOfCloth, TypeOfClothDto>().ReverseMap();
        }
    }
}
