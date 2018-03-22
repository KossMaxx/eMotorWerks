using System.Threading.Tasks;
using System.Web.Mvc;
using Libraries.Core.Backend.Common;

namespace Modules.Site.DetailsRole
{
    public interface IDetailsRoleController
    {
        Task<ActionResult> Add();
        Task<ActionResult> Update(string roleName);
    }

    [Authorize]
    public class DetailsRoleController : BaseController<IDetailsRoleRepository>, IDetailsRoleController
    {
        public DetailsRoleController(IDetailsRoleRepository detailsRoleRepository):base(detailsRoleRepository)
        {
        }

        public async Task<ActionResult> Add()
        {
            var model = Repository.GetNewModel();
            return await GeneratorActionResult("~/DetailsRole/Index.cshtml", model);
        }

        public async Task<ActionResult> Update(string roleName)
        {
            var decodeRoleName = Server.UrlDecode(roleName);
            var model = Repository.GetUpdateModel(decodeRoleName);
            return await GeneratorActionResult("~/DetailsRole/Index.cshtml", model);
        }
    }
}