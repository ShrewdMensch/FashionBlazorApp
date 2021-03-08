using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionAppBlazor.Client.Services.Contract
{
    public interface IIncurredExpenseService
    {
        public Task<IncurredExpenseDto> GetIncurredExpense(Guid id);
        public Task<IEnumerable<IncurredExpenseDto>> GetIncurredExpenses();
        public Task<IncurredExpenseDto> CreateOrUpdateIncurredExpense(IncurredExpenseDto operatingExpense);
        public Task DeleteIncurredExpense(Guid id);
        public Task DeleteAllIncurredExpenses();
    }
}
