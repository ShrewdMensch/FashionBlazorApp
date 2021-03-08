using System.Collections.Generic;

namespace Domain
{
    public class CustomWearOrder 
    {
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
