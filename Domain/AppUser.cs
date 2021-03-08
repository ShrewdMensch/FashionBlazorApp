using Microsoft.AspNetCore.Identity;
using System;

namespace Domain
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public virtual Address  Address{ get; set; }

        public string AlternativePhoneNumber { get; set; }

        public string IGHandle { get; set; }

        public virtual AppUserPhoto Photo { get; set; }
    }
}
