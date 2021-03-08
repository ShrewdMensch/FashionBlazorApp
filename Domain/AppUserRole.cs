using Microsoft.AspNetCore.Identity;
using System;

namespace Domain
{
    public class AppUserRole : IdentityRole<Guid>
    {
        public AppUserRole()
        {
        }

        public AppUserRole(string role):base(role)
        {
        }
    }
}
