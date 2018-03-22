using System;
using System.Collections.Generic;
using Dal;

namespace Libraries.Core.Backend.Authorization
{
    public interface IIdentityRepository
    {
        void Initialize();
        void CreateDefaultRoles();
        AccountModel GetUser(string userName);
        void CreateUser(AccountModel model, string password);
        void UpdateUser(AccountModel model);
        void DeleteUser(string userName);
        void AddUserInRoles(string userName, string[] roles);
        void RemoveUserInRoles(string userName, string[] roles);
        List<string> GetAllRoles();
        List<AccountModel> GetAllUsers();
        List<string> GetRolesForRole(string roleName);
        List<string> GetRolesForUser(string userName);
        void AddRole(string role);
        void AddRoleInRoles(string role, string[] baseRoles);
        void UpdateRoleInRoles(string role, string[] baseRoles);
        void AddChildrenRoles(string parent, List<string> children);
        void RemoveRole(string role);
        void RemoveChildrenRoles(string parent, List<string> children);
        void CreateDefaultAdministrator();
        IDalContext Context { get; set; }
    }
}
