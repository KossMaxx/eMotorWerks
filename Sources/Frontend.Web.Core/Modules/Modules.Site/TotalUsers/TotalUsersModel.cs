using System.Collections.Generic;
using System.Data.Entity;

namespace Modules.Site.TotalUsers
{
    public class TotalUserListElement
    {
        public TotalUserListElement(long id, string name, string email, string description, string note)
        {
            Id = id;
            Name = name;
            Email = email;
            Description = description;
            Note = note;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }
    public class TotalUsersModel
    {
        public TotalUsersModel(List<TotalUserListElement> users)
        {
            Users = users;
        }

        public List<TotalUserListElement> Users { get; set; }
    }
}
