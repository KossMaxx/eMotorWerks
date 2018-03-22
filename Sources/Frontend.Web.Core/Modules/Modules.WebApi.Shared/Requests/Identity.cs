using System.Collections.Generic;
using Dal;
using Modules.WebApi.Shared.Requests;

namespace Modules.WebApi.Shared.Identity.Requests
{
    public interface IUserNameField { string UserName { get; set; } }
    public interface IDescriptionField { string Description { get; set; } }
    public interface IEmailField { string Email { get; set; } }
    public interface INoteField { string Note { get; set; } }
    public interface IRoleField { string Role { get; set; } }
    public interface IRoleNameField { string RoleName { get; set; } }
    public interface IRolesField { string[] Roles { get; set; } }
    public interface IBaseRolesField { string[] BaseRoles { get; set; } }
    public interface IParentField { string Parent { get; set; } }
    public interface IChildrenField { List<string> Children { get; set; } }

    public class UpdateUserRequest : CreateUserRequest { }
    public class CreateUserRequest:BaseRequest, IUserNameField, IDescriptionField, IEmailField, INoteField, IBaseRolesField
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string[] BaseRoles { get; set; }
    }

    public class DeleteUserRequest : BaseRequest, IUserNameField
    {
        public string UserName { get; set; }
    };
    public class GetRolesForUserRequest : DeleteUserRequest { }

    public class AddUserInRolesRequest:BaseRequest, IUserNameField, IRolesField
    {
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
    public class RemoveUserInRolesRequest : AddUserInRolesRequest { }

    public class GetRolesForRoleRequest : BaseRequest, IRoleField
    {
        public string Role { get; set; }
    }

    public class UpdateRoleRequest : AddRoleRequest { }
    public class AddRoleRequest : BaseRequest, IRoleNameField, IBaseRolesField 
    {
        public string RoleName { get; set; }
        public string[] BaseRoles { get; set; }
    }
    public class RemoveRoleRequest : GetRolesForRoleRequest { }

    public class AddChildrenRolesRequest : BaseRequest, IParentField, IChildrenField
    {
        public string Parent { get; set; }
        public List<string> Children { get; set; }
    }
    public class RemoveChildrenRolesRequest : AddChildrenRolesRequest { }
}
