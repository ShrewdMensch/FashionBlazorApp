using System;

namespace Application.DTOs
{
    public class TypeOfClothMeasurementHeaderDto
    {
        public Guid Id { get; set; }

        public Guid MeasurementHeaderId { get; set; }

        public string MeasurementHeader { get; set; }

    }
}