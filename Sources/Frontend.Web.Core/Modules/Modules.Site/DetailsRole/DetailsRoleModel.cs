using System.Collections.Generic;

namespace Modules.Site.DetailsRole
{
    public class BaseRoleElement
    {
        public BaseRoleElement(string roleName, bool isSelectable)
        {
            RoleName = roleName;
            IsSelectable = isSelectable;
        }

        public BaseRoleElement(string roleName):this(roleName, false){}

        public string RoleName { get; set; }
        public bool IsSelectable { get; set; }
    }
    public class DetailsRoleModel
    {
        public DetailsRoleModel(string roleName, bool isUpdate, List<BaseRoleElement> roles)
        {
            IsUpdate = isUpdate;
            RoleName = roleName;
            Roles = roles;
        }

        public string RoleName { get; set; }
        public bool IsUpdate { get; set; }
        public List<BaseRoleElement> Roles { get; set; }
    }
}
