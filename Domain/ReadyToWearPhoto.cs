using System;

namespace Domain
{
    public class ReadyToWearPhoto : Photo
    {
        public virtual ReadyToWear ReadyToWear { get; set; }

        public Guid ReadyToWearId { get; set; }
    }
}
