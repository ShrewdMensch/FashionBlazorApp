using Application.DTOs;
using Application.Repository;
using AutoMapper;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application.AutoMapper.ValueResolvers
{
    public class ReadyToWearClothTotalCostResolver : IValueResolver<ReadyToWear, ReadyToWearDto, double>
    {
        private readonly IRepository _repository;
        public ReadyToWearClothTotalCostResolver(IRepository repository)
        {
            _repository = repository;

        }
        public double Resolve(ReadyToWear source, ReadyToWearDto destination, double destMember, ResolutionContext context)
        {
            var productionDays = source.TypeOfCloth.ProductionDays;
            List<string> photoUrls = new List<string>();

            var totalOperatingExpenses = (_repository.GetAll<OperatingExpense>().Result).Sum(o => o.CostPerDay) * productionDays;

            var totalIncurredExpenses = source.TypeOfCloth.IncurredExpenses.Sum(o => o.IncurredExpense.Cost);

            var dailyRate = (_repository.GetAll<DailyRate>().Result).FirstOrDefault().Cost * productionDays;

            var cost = source.TotalCostOfAccessories + totalOperatingExpenses + dailyRate + totalIncurredExpenses;

            foreach (var photo in source.Photos)
            {
                photoUrls.Add(photo.Url);
            }

            destination.Photos = photoUrls;

            destination.MainPhoto = source.Photos?.FirstOrDefault()?.Url;


            return cost;
        }
    }
}
