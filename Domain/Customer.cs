using System.Collections.Generic;

namespace Domain
{
    public class Customer : AppUser
    {
        public virtual ICollection<RegularOrder> RegularOrders { get; set; }
    }
}
