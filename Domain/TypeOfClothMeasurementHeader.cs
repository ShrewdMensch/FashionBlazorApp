using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TypeOfClothMeasurementHeader
    {
        public Guid Id { get; set; }

        public Guid TypeOfClothId { get; set; }

        public Guid MeasurementHeaderId { get; set; }

        public virtual MeasurementHeader MeasurementHeader { get; set; }

        public virtual TypeOfCloth TypeOfCloth { get; set; }
    }
}
