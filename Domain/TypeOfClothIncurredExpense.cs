using System;

namespace Domain
{
    public class TypeOfClothIncurredExpense
    {
        public Guid Id { get; set; }

        public Guid TypeOfClothId { get; set; }

        public Guid IncurredExpenseId { get; set; }

        public virtual IncurredExpense IncurredExpense { get; set; }

        public virtual TypeOfCloth TypeOfCloth { get; set; }
    }
}
