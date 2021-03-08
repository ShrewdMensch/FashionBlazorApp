using Application.DTOs;
using Application.InputModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IOperatingExpenseService
    {
        public Task<OperatingExpenseDto> GetOperatingExpense(Guid id);
        public Task<IEnumerable<OperatingExpenseDto>> GetOperatingExpenses();
        public Task<OperatingExpenseDto> CreateOrUpdateOperatingExpense(OperatingExpenseInputModel operatingExpenseInput);
        public Task DeleteOperatingExpense(Guid id);
        public Task DeleteAllOperatingExpenses();
    }
}
