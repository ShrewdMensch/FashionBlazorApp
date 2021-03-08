using Application.Comparer;
using Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Application.Extensions
{
    public static class AppDomainExtensions
    {
        public static IEnumerable<AccessoryWithQuantityDto> GetDistinctAccessory(this IEnumerable<AccessoryWithQuantityDto> thisAccessory)
        {
            return thisAccessory.OrderByDescending(a => a.Quantity).Distinct(new AccessoryWithQuantityComparer());
        }
    }

}