using System;

namespace Domain
{
    public class RegularCartItem : CartItem
    {
        public RegularCartItem()
        {
            Quantity = 1;
        }

        public double UnitPrice { get; set; }

        public double TotalPrice => UnitPrice * Quantity;

        public int Quantity { get; set; }

        public Guid TypeOfClothId { get; set; }

        public virtual TypeOfCloth TypeOfCloth { get; set; }
    }
}
