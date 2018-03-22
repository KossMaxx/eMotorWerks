using System.Collections.Generic;
using Modules.Site.DetailsRole;

namespace Modules.Site.DetailsUser
{
    public class DetailsUserModel
    {
        public DetailsUserModel(string userName, string description, string email, string note, bool isUpdate, List<BaseRoleElement> roles)
        {
            UserName = userName;
            Description = description;
            Email = email;
            Note = note;
            IsUpdate = isUpdate;
            Roles = roles;
        }

        public string UserName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public bool IsUpdate { get; set; }
        public List<BaseRoleElement> Roles { get; set; }
    }
}
