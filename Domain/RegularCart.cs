using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class RegularCart : Cart
    {
        public double GrandTotal => RegularCartItems.Sum(i => i.TotalPrice);

        public virtual ICollection<RegularCartItem> RegularCartItems { get; set; }

    }
}
