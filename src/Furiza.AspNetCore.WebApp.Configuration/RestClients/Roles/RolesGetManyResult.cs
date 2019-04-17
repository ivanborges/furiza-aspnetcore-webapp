using System.Collections.Generic;

namespace Furiza.AspNetCore.WebApp.Configuration.RestClients.Roles
{
    public class RolesGetManyResult
    {
        public IEnumerable<RolesGetManyResultInnerRole> Roles { get; set; }

        public class RolesGetManyResultInnerRole
        {
            public string RoleName { get; set; }
        }
    }
}