using AutoMapper;
using Domain;
using Application.DTOs;
using Application.InputModels;
using System;
using Domain.Utility;

namespace Application.AutoMapper.MappingProfiles
{
    public class OperatingExpenseMappingProfile : Profile
    {
        public OperatingExpenseMappingProfile()
        {
            CreateMap<OperatingExpense, OperatingExpenseDto>()
                .ForMember(destination => destination.Type, option => option.MapFrom(p => p.Type.ToString()));

            CreateMap<OperatingExpense, OperatingExpenseInputModel>().ReverseMap();

            CreateMap<OperatingExpenseDto, OperatingExpenseInputModel>()
                .ForMember(destination => destination.Type, option => option
                  .MapFrom(p => Enum.Parse(typeof(OperatingExpenseType), p.Type, true)));
        }
    }
}
