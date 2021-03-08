using Domain.Utility;
using System;

namespace Domain
{
    public class Cart
    {
        public string Id { get; set; }

        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
