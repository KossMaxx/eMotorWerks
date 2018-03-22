using System.Linq;
using log4net;
using Libraries.Core.Backend.Authorization;
using Libraries.Core.Backend.Common;
using Project.Kernel;

namespace Modules.Site.TotalUsers
{
    public interface ITotalUsersRepository
    {
        TotalUsersModel GetAllUsers();
        IIdentityRepository IdentityRepository { get; set; }
    }

    public class TotalUsersRepository : BaseRepository, ITotalUsersRepository
    {
        public TotalUsersRepository(IWrapper<ILog> logger, IIdentityRepository identityRepository) : base(logger)
        {
            IdentityRepository = identityRepository;
        }

        public TotalUsersModel GetAllUsers()
        {
            var users = IdentityRepository.GetAllUsers().Select(u =>
                new TotalUserListElement(u.Id, u.AccountName, u.Email, u.Description, u.Note)).ToList();
            return new TotalUsersModel(users);
        }

        public IIdentityRepository IdentityRepository { get; set; }
    }
}
