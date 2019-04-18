using System;
using System.Collections.Generic;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Users
{
    public class UsersGetManyResult
    {
        public IEnumerable<UsersGetManyResultInnerUser> Users { get; set; }

        public class UsersGetManyResultInnerUser
        {
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string HiringType { get; set; }
            public string Company { get; set; }
            public string Department { get; set; }
            public IEnumerable<UsersGetManyResultInnerClaim> Claims { get; set; }
            public IEnumerable<UsersGetManyResultInnerRole> Roles { get; set; }
            public bool? EmailConfirmed { get; set; }
            public bool? LockoutEnabled { get; set; }
            public DateTime? LockoutEnd { get; set; }

            public bool LockedOut => LockoutEnabled.HasValue && LockoutEnabled.Value && LockoutEnd.HasValue && LockoutEnd.Value.Date > DateTime.Now.Date;
        }

        public class UsersGetManyResultInnerClaim
        {
            public string Type { get; set; }
            public string Value { get; set; }
        }

        public class UsersGetManyResultInnerRole
        {
            public string RoleName { get; set; }
        }
    }
}