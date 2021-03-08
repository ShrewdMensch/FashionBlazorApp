using AutoMapper;
using Domain;
using Application.DTOs;

namespace Application.AutoMapper.MappingProfiles
{
    public class IncurredExpenseMappingProfile : Profile
    {
        public IncurredExpenseMappingProfile()
        {
            CreateMap<IncurredExpense, IncurredExpenseDto>()
                .ForMember(destination => destination.Name, option => option.MapFrom(p => p.Name))
                .ForMember(destination => destination.Id, option => option.MapFrom(p => p.Id))
                .ForMember(destination => destination.Cost, option => option.MapFrom(p => p.Cost)).ReverseMap();
        }
    }
}
