using Libraries.Core.Backend.Common;
using Microsoft.Practices.Unity;
using Modules.Site.DetailsRole;
using Modules.Site.DetailsUser;
using Modules.Site.Home;
using Modules.Site.TotalRoles;
using Modules.Site.TotalUsers;

namespace Modules.Site
{
    public class TypeFabric:BaseTypeFabric
    {
        public override void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IHomeRepository, HomeRepository>();
            container.RegisterType<ITotalRolesRepository, TotalRolesRepository>();
            container.RegisterType<ITotalUsersRepository, TotalUsersRepository>();
            container.RegisterType<IDetailsRoleRepository, DetailsRoleRepository>();
            container.RegisterType<IDetailsUserRepository, DetailsUserRepository>();

            container.RegisterType<IHomeController, HomeController>();
            container.RegisterType<ITotalRolesController, TotalRolesController>();
            container.RegisterType<ITotalUsersController, TotalUsersController>();
            container.RegisterType<IDetailsRoleController, DetailsRoleController>();
            container.RegisterType<IDetailsUserController, DetailsUserController>();
        }
    }
}
