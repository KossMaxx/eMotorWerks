using System.Collections.Generic;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.Common;
using Project.Kernel;

namespace Modules.Site.TotalRoles
{
    public interface ITotalRolesRepository
    {
        IIdentityRepository IdentityRepository { get; set; }
        TotalRolesModel GetAllRoles();
    }

    public class TotalRolesRepository : BaseRepository, ITotalRolesRepository
    {
        public TotalRolesRepository(IIdentityRepository identityRepository, IWrapper<ILog> logger) : base(logger)
        {
            IdentityRepository = identityRepository;
        }

        public TotalRolesModel GetAllRoles()
        {
            var roles = IdentityRepository.GetAllRoles();
            return new TotalRolesModel(roles);
        }

        public IIdentityRepository IdentityRepository { get; set; }
    }
}
