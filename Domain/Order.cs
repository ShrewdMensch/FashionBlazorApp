using Domain.Utility;
using System;

namespace Domain
{
    public class Order
    {
        public string Id { get; set; }

        public Guid CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public OrderStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

    }
}
