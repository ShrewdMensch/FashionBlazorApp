using System.Collections.Generic;
using System.Linq;

namespace Application.DTOs
{
    public class TypeOfClothDto : EntityDto
    {
        public int ProductionDays { get; set; }

        public int TotalNumberOfAccesories => Accessories.Sum(a=>a.Quantity);

        public IEnumerable<TypeOfClothMeasurementHeaderDto> MeasurementHeaders { get; set; }

        public IEnumerable<TypeOfClothAccessoryDto> Accessories { get; set; }

        public IEnumerable<TypeOfClothIncurredExpenseDto> IncurredExpenses { get; set; }

    }
}