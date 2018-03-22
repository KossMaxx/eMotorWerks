using System.Threading.Tasks;
using System.Web.Mvc;
using Libraries.Core.Backend.Common;

namespace Modules.Site.DetailsUser
{
    public interface IDetailsUserController
    {
        Task<ActionResult> New();
        Task<ActionResult> Update(string userName);
    }

    [Authorize]
    public class DetailsUserController : BaseController<IDetailsUserRepository>, IDetailsUserController
    {
        public DetailsUserController(IDetailsUserRepository detailsUserRepository):base(detailsUserRepository)
        {
        }

        public async Task<ActionResult> New()
        {
            var model = Repository.GetNewModel();
            return await GeneratorActionResult("~/DetailsUser/Index.cshtml", model);
        }

        public async Task<ActionResult> Update(string userName)
        {
            var decodeUserName = Server.UrlDecode(userName);
            var model = Repository.GetUpdateModel(decodeUserName);
            return await GeneratorActionResult("~/DetailsUser/Index.cshtml", model);
        }
    }
}