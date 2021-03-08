using Application.DTOs;
using Application.InputModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface ITypeOfClothService
    {
        public Task<TypeOfClothDto> GetTypeOfCloth(Guid id);
        public Task<IEnumerable<TypeOfClothDto>> GetTypeOfCloths();
        public Task<TypeOfClothDto> CreateOrUpdateTypeOfCloth(TypeOfClothInputModel typeOfClothInput);
        public Task DeleteTypeOfCloth(Guid id);
        public Task DeleteAllTypeOfCloths();
    }
}
