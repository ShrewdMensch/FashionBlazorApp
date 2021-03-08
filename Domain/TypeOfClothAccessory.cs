using System;

namespace Domain
{
    public class TypeOfClothAccessory
    {
        public Guid Id { get; set; }

        public Guid AccessoryId { get; set; }

        public Guid TypeOfClothId { get; set; }

        public int Quantity { get; set; }

        public double TotalCost => Accessory.Cost * Quantity;

        public virtual Accessory Accessory { get; set; }

        public virtual TypeOfCloth TypeOfCloth { get; set; }
    }
}
