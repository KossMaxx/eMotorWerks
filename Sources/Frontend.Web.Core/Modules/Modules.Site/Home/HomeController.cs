using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Libraries.Core.Backend.Common;

namespace Modules.Site.Home
{
    public interface IHomeController
    {
        Task<ActionResult> Index();
    }

    [Authorize]
    public class HomeController:BaseController<IHomeRepository>, IHomeController
    {
        public HomeController(IHomeRepository homeRepository):base(homeRepository)
        {
        }

        public async Task<ActionResult> Index()
        {
            var model = Repository.GetLoadModules();
            return await GeneratorActionResult("~/Home/Index.cshtml", model);
        }

    }
}