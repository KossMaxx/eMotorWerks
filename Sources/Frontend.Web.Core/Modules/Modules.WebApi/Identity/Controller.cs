using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.WebApi;
using Libraries.Core.Backend.WebApi.Repositories;
using Modules.WebApi.Shared.Identity.Requests;
using Modules.WebApi.Shared.Requests;
using Project.Kernel;

namespace Modules.WebApi.Identity
{
    public interface IIdentityApiController
    {
        Task<HttpResponseMessage> CreateUser(CreateUserRequest request);
        Task<HttpResponseMessage> DeleteUser(DeleteUserRequest request);
        Task<HttpResponseMessage> UpdateUser(UpdateUserRequest request);
        Task<HttpResponseMessage> AddUserInRoles(AddUserInRolesRequest request);
        Task<HttpResponseMessage> RemoveUserInRoles(RemoveUserInRolesRequest request);
        Task<HttpResponseMessage> GetAllRoles(BaseRequest request);
        Task<HttpResponseMessage> GetAllUsers(BaseRequest request);
        Task<HttpResponseMessage> GetRolesForRole(GetRolesForRoleRequest request);
        Task<HttpResponseMessage> GetRolesForUser(GetRolesForUserRequest request);
        Task<HttpResponseMessage> AddRole(AddRoleRequest request);
        Task<HttpResponseMessage> UpdateRole(UpdateRoleRequest request);
        Task<HttpResponseMessage> AddChildrenRoles(AddChildrenRolesRequest request);
        Task<HttpResponseMessage> RemoveRole(RemoveRoleRequest request);
        Task<HttpResponseMessage> RemoveChildrenRoles(RemoveChildrenRolesRequest request);
    }

    [RoutePrefix("identity")]
    public class IdentityApiController : BaseApiController<IIdentityApiRepository>, IIdentityApiController
    {

        public IdentityApiController(IIdentityApiRepository repository, IVersionRepository versionRepository, Wrapper<ILog> logger) : base(repository, versionRepository, logger)
        {
        }

        [Route("createuser")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]

        public Task<HttpResponseMessage> CreateUser([FromBody] CreateUserRequest request)
        {
            return ExecuteAction(request, Repository.CreateUser);
        }

        [Route("updateuser")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]

        public Task<HttpResponseMessage> UpdateUser([FromBody] UpdateUserRequest request)
        {
            return ExecuteAction(request, Repository.UpdateUser);
        }

        [Route("deleteuser")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> DeleteUser([FromBody] DeleteUserRequest request)
        {
            return ExecuteAction(request, Repository.DeleteUser);
        }

        [Route("adduserinroles")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> AddUserInRoles([FromBody] AddUserInRolesRequest request)
        {
            return ExecuteAction(request, Repository.AddUserInRoles);
        }

        [Route("removeuserinroles")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> RemoveUserInRoles([FromBody] RemoveUserInRolesRequest request)
        {
            return ExecuteAction(request, Repository.RemoveUserInRoles);
        }

        [Route("getallroles")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> GetAllRoles([FromBody] BaseRequest request)
        {
            return ExecuteAction(request, Repository.GetAllRoles);
        }

        [Route("getallusers")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> GetAllUsers([FromBody] BaseRequest request)
        {
            return ExecuteAction(request, Repository.GetAllUsers);
        }

        [Route("getrolesforrole")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> GetRolesForRole([FromBody] GetRolesForRoleRequest request)
        {
            return ExecuteAction(request, Repository.GetRolesForRole);
        }

        [Route("getrolesforuser")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> GetRolesForUser([FromBody] GetRolesForUserRequest request)
        {
            return ExecuteAction(request, Repository.GetRolesForUser);
        }

        [Route("addrole")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> AddRole([FromBody] AddRoleRequest request)
        {
            return ExecuteAction(request, Repository.AddRole);
        }

        [Route("updaterole")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> UpdateRole(UpdateRoleRequest request)
        {
            return ExecuteAction(request, Repository.UpdateRole);
        }

        [Route("addchildrenroles")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> AddChildrenRoles([FromBody] AddChildrenRolesRequest request)
        {
            return ExecuteAction(request, Repository.AddChildrenRoles);
        }

        [Route("removerole")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> RemoveRole([FromBody] RemoveRoleRequest request)
        {
            return ExecuteAction(request, Repository.RemoveRole);
        }

        [Route("removechildrenroles")]
        [HttpPost]
        [Authorize(Roles = ERoles.Administrator)]
        public Task<HttpResponseMessage> RemoveChildrenRoles([FromBody] RemoveChildrenRolesRequest request)
        {
            return ExecuteAction(request, Repository.RemoveChildrenRoles);
        }
    }
}