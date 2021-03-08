using System;
using System.Collections.Generic;

namespace Domain
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public string Size { get; set; }

        public virtual ICollection<Measurement> Measurements { get; set; }

        ///<summary>
        ///Could be ReadyMade, NewWear or CustomerPreference
        ///</summary>
        public string Variant { get; set; }
    }
}
