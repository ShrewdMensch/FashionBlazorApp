using System;

namespace Application.DTOs
{
    public class TypeOfClothAccessoryDto
    {
        public Guid Id { get; set; }

        public Guid AccessoryId { get; set; }

        public int Quantity { get; set; }

        public double TotalCost { get; set; }

        public string Accessory { get; set; }

    }
}