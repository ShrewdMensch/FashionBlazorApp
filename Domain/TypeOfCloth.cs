using System.Collections.Generic;

namespace Domain
{
    public class TypeOfCloth : Entity
    {
        public double ProductionDays { get; set; }

        public virtual ICollection<TypeOfClothMeasurementHeader> MeasurementHeaders { get; set; }

        public virtual ICollection<TypeOfClothAccessory> Accessories{ get; set; }

        public virtual ICollection<TypeOfClothIncurredExpense> IncurredExpenses { get; set; }
    }
}
