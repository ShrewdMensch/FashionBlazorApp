using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class RegularOrder : Order
    {
        public double GrandTotal => RegularOrderItems.Sum(i => i.TotalPrice);

        public virtual ICollection<RegularOrderItem> RegularOrderItems { get; set; }

    }
}
