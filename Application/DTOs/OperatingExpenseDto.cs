using System;

namespace Application.DTOs
{
    public class OperatingExpenseDto : EntityDto
    {
        public double Quantity { get; set; }

        public double TotalCost { get; set; }

        public double CostPerDay { get; set; }

        public string Type { get; set; }

    }
}