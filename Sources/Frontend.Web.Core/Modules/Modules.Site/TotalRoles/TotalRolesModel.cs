using System.Collections.Generic;

namespace Modules.Site.TotalRoles
{
    public class TotalRolesModel
    {
        public TotalRolesModel(List<string> roles)
        {
            Roles = roles;
        }

        public List<string> Roles { get; set; }
    }
}
