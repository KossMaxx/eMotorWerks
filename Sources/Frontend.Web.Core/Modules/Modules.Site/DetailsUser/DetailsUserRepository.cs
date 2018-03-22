using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.Common;
using Microsoft.Practices.ObjectBuilder2;
using Modules.Site.DetailsRole;
using Project.Kernel;

namespace Modules.Site.DetailsUser
{
    public interface IDetailsUserRepository
    {
        DetailsUserModel GetNewModel();
        DetailsUserModel GetUpdateModel(string userName);
        IIdentityRepository IdentityRepository { get; set; }
    }

    public class DetailsUserRepository : BaseRepository, IDetailsUserRepository
    {
        public DetailsUserRepository(IWrapper<ILog> logger, IIdentityRepository identityRepository) : base(logger)
        {
            IdentityRepository = identityRepository;
        }

        public DetailsUserModel GetNewModel()
        {
            var roles = IdentityRepository.GetAllRoles().Select(role => new BaseRoleElement(role)).ToList();
            return new DetailsUserModel(string.Empty, string.Empty, string.Empty, string.Empty, false, roles);
        }

        public DetailsUserModel GetUpdateModel(string userName)
        {
            var rolesForPage = new List<BaseRoleElement>();
            var roles = IdentityRepository.GetAllRoles();
            var account = IdentityRepository.GetUser(userName);
            var userRoles = IdentityRepository.GetRolesForUser(userName);
            userRoles.ForEach(ur => rolesForPage.Add(new BaseRoleElement(ur, true)));
            roles.Except(userRoles).ForEach(role => rolesForPage.Add(new BaseRoleElement(role)));
            return new DetailsUserModel(account.AccountName, account.Description, account.Email, account.Note, true, rolesForPage);
        }

        public IIdentityRepository IdentityRepository { get; set; }
    }
}
