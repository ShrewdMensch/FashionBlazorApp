using System;

namespace Application.DTOs
{
    public class TypeOfClothIncurredExpenseDto
    {
        public Guid Id { get; set; }

        public Guid IncurredExpenseId { get; set; }

        public string IncurredExpenseName { get; set; }

        public double Cost { get; set; }

    }
}