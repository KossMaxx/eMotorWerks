using System.Collections.Generic;
using System.Linq;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.Common;
using Microsoft.Practices.ObjectBuilder2;
using Project.Kernel;

namespace Modules.Site.DetailsRole
{
    public interface IDetailsRoleRepository
    {
        DetailsRoleModel GetNewModel();
        DetailsRoleModel GetUpdateModel(string roleName);
        IIdentityRepository IdentityRepository { get; set; }
    }

    public class DetailsRoleRepository : BaseRepository, IDetailsRoleRepository
    {
        public DetailsRoleRepository(IWrapper<ILog> logger, IIdentityRepository identityRepository) : base(logger)
        {
            IdentityRepository = identityRepository;
        }

        public DetailsRoleModel GetNewModel()
        {
            var roles = IdentityRepository.GetAllRoles().Select(role => new BaseRoleElement(role)).ToList();
            return new DetailsRoleModel(string.Empty, false, roles);
        }

        public DetailsRoleModel GetUpdateModel(string roleName)
        {
            var rolesForPage = new List<BaseRoleElement>();
            var allRoles = IdentityRepository.GetAllRoles();
            allRoles.Remove(roleName);
            var baseRoles = IdentityRepository.GetRolesForRole(roleName);
            baseRoles.ForEach(role => rolesForPage.Add(new BaseRoleElement(role, true)));
            allRoles.Except(baseRoles).ForEach(role => rolesForPage.Add(new BaseRoleElement(role)));
            return new DetailsRoleModel(roleName, true, rolesForPage);
        }

        public IIdentityRepository IdentityRepository { get; set; }
    }
}
