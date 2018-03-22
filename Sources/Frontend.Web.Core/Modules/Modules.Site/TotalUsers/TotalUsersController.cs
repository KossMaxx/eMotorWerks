using System.Threading.Tasks;
using System.Web.Mvc;
using Libraries.Core.Backend.Common;

namespace Modules.Site.TotalUsers
{
    public interface ITotalUsersController
    {
        Task<ActionResult> Index();
    }

    [Authorize]
    public class TotalUsersController : BaseController<ITotalUsersRepository>, ITotalUsersController
    {
        public TotalUsersController(ITotalUsersRepository totalUsersRepository):base(totalUsersRepository)
        {
        }

        public async Task<ActionResult> Index()
        {
            var model = Repository.GetAllUsers();
            return await GeneratorActionResult("~/TotalUsers/Index.cshtml", model);
        }

    }
}