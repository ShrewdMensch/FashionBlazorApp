using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ReadyToWear : Entity
    {
        public Guid TypeOfClothId { get; set; }

        public virtual ICollection<ReadyToWearPhoto> Photos { get; set; }

        public virtual TypeOfCloth TypeOfCloth { get; set; }

        public int NumberInStock { get; set; }

        public double TotalCostOfAccessories => GetCost();

        public bool OutOfStock => NumberInStock < 1;

        private double GetCost()
        {
            var costOfAccessories = TypeOfCloth.Accessories.Sum(accessory => accessory.TotalCost);

            return costOfAccessories;
        }
    }
}
