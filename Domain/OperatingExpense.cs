using Domain.Utility;

namespace Domain
{
    public class OperatingExpense : Entity
    {
        public OperatingExpense()
        {
            Quantity = 1;
            Type = OperatingExpenseType.Daily;
        }
        public double TotalCost { get; set; }

        public int Quantity { get; set; }

        public OperatingExpenseType Type { get; set; }

        public double CostPerDay => GetCostPerDay();

        private double GetCostPerDay()
        {
            if (Type == OperatingExpenseType.Monthly) return TotalCost / 30;
            if (Type == OperatingExpenseType.Yearly) return TotalCost / 365;

            return TotalCost;
        }
    }
}
