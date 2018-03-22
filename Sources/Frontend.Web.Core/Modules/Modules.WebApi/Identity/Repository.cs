using System.Configuration;
using System.Linq;
using Dal;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.Common;
using Modules.WebApi.Shared.Identity.Requests;
using Modules.WebApi.Shared.Identity.Responses;
using Modules.WebApi.Shared.Requests;
using Modules.WebApi.Shared.Responses;
using Project.Kernel;


namespace Modules.WebApi.Identity
{
    public interface IIdentityApiRepository
    {
        BaseResponse CreateUser(CreateUserRequest request);
        BaseResponse UpdateUser(CreateUserRequest request);
        BaseResponse DeleteUser(DeleteUserRequest request);
        BaseResponse AddUserInRoles(AddUserInRolesRequest request);
        BaseResponse RemoveUserInRoles(RemoveUserInRolesRequest request);
        IdentityResponse GetAllRoles(BaseRequest request);
        IdentityResponse GetAllUsers(BaseRequest request);
        IdentityResponse GetRolesForRole(GetRolesForRoleRequest request);
        IdentityResponse GetRolesForUser(GetRolesForUserRequest request);
        BaseResponse AddRole(AddRoleRequest request);
        BaseResponse UpdateRole(UpdateRoleRequest request);
        BaseResponse AddChildrenRoles(AddChildrenRolesRequest request);
        BaseResponse RemoveRole(RemoveRoleRequest request);
        BaseResponse RemoveChildrenRoles(RemoveChildrenRolesRequest request);
        IIdentityRepository IdentityRepository { get; set; }
    }

    public class IdentityApiRepository : BaseRepository, IIdentityApiRepository
    {
        public IdentityApiRepository(IWrapper<ILog> logger, IIdentityRepository identityRepository) : base(logger)
        {
            IdentityRepository = identityRepository;
        }

        public BaseResponse CreateUser(CreateUserRequest request)
        {
            var accountModel = new AccountModel
            {
                IsActivate = true,
                AccountName = request.UserName,
                Description = request.Description,
                Email = request.Email,
                Note = request.Note
            };
            var baseRoles = (request.BaseRoles ?? Enumerable.Empty<string>()).ToArray();
            IdentityRepository.CreateUser(accountModel, "!@#$%^&*()");
            IdentityRepository.AddUserInRoles(request.UserName, baseRoles);
            return BaseResponse.Ok();
        }

        public BaseResponse UpdateUser(CreateUserRequest request)
        {
            var accountModel = new AccountModel
            {
                IsActivate = true,
                AccountName = request.UserName,
                Description = request.Description,
                Email = request.Email,
                Note = request.Note
            };
            IdentityRepository.UpdateUser(accountModel);
            var baseRoles = (request.BaseRoles ?? Enumerable.Empty<string>()).ToArray();
            var userRoles =IdentityRepository.GetRolesForUser(request.UserName);
            IdentityRepository.RemoveUserInRoles(request.UserName, userRoles.ToArray());
            IdentityRepository.AddUserInRoles(request.UserName, baseRoles);
            return BaseResponse.Ok();
        }

        public BaseResponse DeleteUser(DeleteUserRequest request)
        {
            IdentityRepository.DeleteUser(request.UserName);
            return BaseResponse.Ok();
        }

        public BaseResponse AddUserInRoles(AddUserInRolesRequest request)
        {
            IdentityRepository.AddUserInRoles(request.UserName, request.Roles);
            return BaseResponse.Ok();
        }

        public BaseResponse RemoveUserInRoles(RemoveUserInRolesRequest request)
        {
            IdentityRepository.RemoveUserInRoles(request.UserName, request.Roles);
            return BaseResponse.Ok();
        }

        public IdentityResponse GetAllRoles(BaseRequest request)
        {
            var data = IdentityRepository.GetAllRoles();
            return new IdentityResponse(data);
        }

        public IdentityResponse GetAllUsers(BaseRequest request)
        {
            var data = IdentityRepository.GetAllUsers().Select(u => u.AccountName).ToList();
            return new IdentityResponse(data);
        }

        public IdentityResponse GetRolesForRole(GetRolesForRoleRequest request)
        {
            var data = IdentityRepository.GetRolesForRole(request.Role);
            return new IdentityResponse(data);
        }

        public IdentityResponse GetRolesForUser(GetRolesForUserRequest request)
        {
            var data = IdentityRepository.GetRolesForUser(request.UserName);
            return new IdentityResponse(data);
        }

        public BaseResponse AddRole(AddRoleRequest request)
        {
            var roleName = string.IsNullOrWhiteSpace(request.RoleName) ? string.Empty : request.RoleName;
            var baseRoles = (request.BaseRoles ?? Enumerable.Empty<string>()).ToArray();
            IdentityRepository.AddRoleInRoles(roleName, baseRoles);
            return BaseResponse.Ok();
        }

        public BaseResponse UpdateRole(UpdateRoleRequest request)
        {
            var roleName = string.IsNullOrWhiteSpace(request.RoleName) ? string.Empty : request.RoleName;
            var baseRoles = (request.BaseRoles ?? Enumerable.Empty<string>()).ToArray();
            IdentityRepository.UpdateRoleInRoles(roleName, baseRoles);
            return BaseResponse.Ok();
        }

        public BaseResponse AddChildrenRoles(AddChildrenRolesRequest request)
        {
            IdentityRepository.AddChildrenRoles(request.Parent, request.Children);
            return BaseResponse.Ok();
        }

        public BaseResponse RemoveRole(RemoveRoleRequest request)
        {
            IdentityRepository.RemoveRole(request.Role);
            return BaseResponse.Ok();
        }

        public BaseResponse RemoveChildrenRoles(RemoveChildrenRolesRequest request)
        {
            IdentityRepository.RemoveChildrenRoles(request.Parent, request.Children);
            return BaseResponse.Ok();
        }

        public IIdentityRepository IdentityRepository { get; set; }
    }
}
