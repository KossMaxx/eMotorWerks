using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dal;
using Libraries.Core.Backend.Common;

namespace Modules.Site.TotalRoles
{
    public interface ITotalRolesController
    {
        Task<ActionResult> Index();
    }

    [Authorize]
    public class TotalRolesController : BaseController<ITotalRolesRepository>, ITotalRolesController
    {
        public TotalRolesController(ITotalRolesRepository totalRolesRepository):base(totalRolesRepository)
        {
        }

        public async Task<ActionResult> Index()
        {
            var model = Repository.GetAllRoles();
            return await GeneratorActionResult("~/TotalRoles/Index.cshtml", model);
        }
    }
}