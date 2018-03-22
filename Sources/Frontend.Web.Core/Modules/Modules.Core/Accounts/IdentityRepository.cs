using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using Dal;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.Common;
using Project.Kernel;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace Modules.Core.Accounts
{
    public class IdentityRepository : BaseRepository, IIdentityRepository
    {
        public IdentityRepository(IDalContext context, IWrapper<ILog> logger) : base(logger)
        {
            Context = context;
        }

        public void Initialize()
        {
            try
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Accounts", "Id", "AccountName", true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateDefaultRoles()
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            var roles = new List<string> { ERoles.System, ERoles.Owner, ERoles.Administrator, ERoles.User };
            roles.ForEach(role => ExistenceRole(roleProvider, role));
        }

        public AccountModel GetUser(string userName)
        {
            return Context.Accounts.First(
                u => string.Compare(u.AccountName, userName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public void CreateUser(AccountModel model, string password)
        {
            CheckAccountName(model.AccountName);
            var confirmToken = WebSecurity.CreateUserAndAccount(model.AccountName, password);
            WebSecurity.ConfirmAccount(confirmToken);
            UpdateAccount(model);
        }

        public void UpdateUser(AccountModel model)
        {
            UpdateAccount(model);
        }

        public void DeleteUser(string username)
        {
            if (!WebSecurity.UserExists(username)) return;
            var userRoles = Roles.GetRolesForUser(username);
            if (userRoles.Any()) Roles.RemoveUserFromRoles(username, userRoles);
            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(username); 
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(username, true);
        }

        public void AddUserInRoles(string userName, string[] roles)
        {
            if (!WebSecurity.UserExists(userName)) return;
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            var validRoles = roles.Where(role => roleProvider.RoleExists(role)).ToList();
            if (!validRoles.Any()) return;
            roleProvider.AddUsersToRoles(new[] {userName}, validRoles.ToArray());
        }

        public void RemoveUserInRoles(string userName, string[] roles)
        {
            if (!WebSecurity.UserExists(userName)) return;
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            var validRoles = roles.Where(role => roleProvider.RoleExists(role)).ToList();
            if (!validRoles.Any()) return;
            var validRolesForUser = validRoles.Where(role => roleProvider.IsUserInRole(userName, role)).ToArray();
            if (!validRolesForUser.Any()) return;
            roleProvider.RemoveUsersFromRoles(new[] {userName}, validRolesForUser);
        }

        public List<string> GetAllRoles()
        {
            return Roles.GetAllRoles().ToList();
        }

        public List<AccountModel> GetAllUsers()
        {
            return Context.Accounts.ToList();
        }

        public List<string> GetRolesForRole(string roleName)
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            if (!roleProvider.RoleExists(roleName)) return Enumerable.Empty<string>().ToList();
            var role = Context.webpages_Roles.First(r =>
                string.Compare(r.RoleName, roleName, StringComparison.OrdinalIgnoreCase) == 0);
            return role.Parents.Any()
                ? role.Parents.Select(r => r.RoleName).ToList()
                : Enumerable.Empty<string>().ToList();
        }

        public List<string> GetRolesForUser(string userName)
        {
            if (!WebSecurity.UserExists(userName)) return Enumerable.Empty<string>().ToList();
            return Roles.GetRolesForUser(userName).ToList();
        }

        public void AddRole(string role)
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            ExistenceRole(roleProvider, role);
        }

        public void AddRoleInRoles(string role, string[] baseRoles)
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            ExistenceRole(roleProvider, role);
            var childRole = Context.webpages_Roles.First(el =>
                string.Compare(el.RoleName, role, StringComparison.OrdinalIgnoreCase) == 0);
            baseRoles.ForEach(r =>
            {
                ExistenceRole(roleProvider, r);
                var baseRole = Context.webpages_Roles.First(el =>
                    string.Compare(el.RoleName, r, StringComparison.OrdinalIgnoreCase) == 0);
                baseRole.Children.Add(childRole);
            });
            Context.SaveChangesAsync().Wait();
        }

        public void UpdateRoleInRoles(string role, string[] baseRoles)
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            ExistenceRole(roleProvider, role);
            var childRole = Context.webpages_Roles.First(el =>
                string.Compare(el.RoleName, role, StringComparison.OrdinalIgnoreCase) == 0);
            childRole.Parents.ToList().ForEach(parent => parent.Children.Remove(childRole));
            childRole.Parents.Clear();
            Context.SaveChangesAsync().Wait();
            baseRoles.ForEach(r =>
            {
                ExistenceRole(roleProvider, r);
                var baseRole = Context.webpages_Roles.First(el =>
                    string.Compare(el.RoleName, r, StringComparison.OrdinalIgnoreCase) == 0);
                baseRole.Children.Add(childRole);
            });
            Context.SaveChangesAsync().Wait();
        }

        public void AddChildrenRoles(string parent, List<string> children)
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            ExistenceRole(roleProvider, parent);
            children.ForEach(role => ExistenceRole(roleProvider, role));
            var parentRole = Context.webpages_Roles.First(role =>
                string.Compare(role.RoleName, parent, StringComparison.OrdinalIgnoreCase) == 0);
            children.Select(child => Context.webpages_Roles.First(role =>
                string.Compare(role.RoleName, child, StringComparison.OrdinalIgnoreCase) == 0))
                .ForEach(childRole => parentRole.Children.Add(childRole));
            Context.SaveChangesAsync().Wait();
        }

        public void RemoveRole(string role)
        {
            var roleProvider = (SimpleRoleProvider)Roles.Provider;
            if (!roleProvider.RoleExists(role)) return;
            var users = roleProvider.GetUsersInRole(role);
            roleProvider.RemoveUsersFromRoles(users, new[] {role});
            roleProvider.DeleteRole(role, true);
        }

        public void RemoveChildrenRoles(string parent, List<string> children)
        {
            var parentRole = Context.webpages_Roles.First(role =>
                string.Compare(role.RoleName, parent, StringComparison.OrdinalIgnoreCase) == 0);
            children.Select(child => Context.webpages_Roles.First(role =>
                    string.Compare(role.RoleName, parent, StringComparison.OrdinalIgnoreCase) == 0))
                .ForEach(childRole => parentRole.Children.Remove(childRole));
            Context.SaveChangesAsync().Wait();
        }

        public void CreateDefaultUser(string userName, string role)
        {
            try
            {
                if (WebSecurity.UserExists(ConfigurationManager.AppSettings[$"{userName}UserName"])) return;
                var accountModel = new AccountModel
                {
                    IsActivate = true,
                    AccountName = ConfigurationManager.AppSettings[$"{userName}UserName"],
                    Email = ConfigurationManager.AppSettings[$"{userName}EmailAddress"],
                };
                CreateUser(accountModel, ConfigurationManager.AppSettings[$"{userName}Password"]);
                Roles.AddUserToRole(accountModel.AccountName, role);
            }
            catch (MembershipCreateUserException)
            {
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void CreateDefaultAdministrator()
        {
            CreateDefaultUser("DefaultAdministrator", ERoles.Administrator);
        }

        protected void ExistenceRole(SimpleRoleProvider provider, string role)
        {
            if (!provider.RoleExists(role))
                provider.CreateRole(role);
        }

        private static void CheckAccountName(string accountName)
        {
            if (WebSecurity.UserExists(accountName)) throw new MembershipCreateUserException("The email and/or username already exist");
        }

        private void UpdateAccount(AccountModel model)
        {
            var account =
                Context.Accounts.First(
                    x => string.Compare(x.AccountName, model.AccountName, StringComparison.OrdinalIgnoreCase) == 0);
            account.UserId = model.UserId;
            account.Email = model.Email;
            account.Description = model.Description;
            account.Note = model.Note;
            account.IsActivate = model.IsActivate;
            Context.SaveChangesAsync().Wait();
        }

        public IDalContext Context { get; set; }
    }
}