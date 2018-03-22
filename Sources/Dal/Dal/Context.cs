using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dal
{
    public interface IDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void ExecuteTransaction(Action action);
    }

    public interface IExtendedFunctionDalContext
    {
        EnumValueModel FindEnumValue(string type, string name);
    }

    public interface IDalContext : IDbContext, IExtendedFunctionDalContext
    {
        IDbSet<SeedModel> Seeding { get; set; }
        IDbSet<EnumTypeModel> EnumTypes { get; set; }
        IDbSet<EnumValueModel> EnumValues { get; set; }
        IDbSet<AccountModel> Accounts { get; set; }
        IDbSet<webpages_Membership> webpages_Membership { get; set; }
        IDbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        IDbSet<webpages_Roles> webpages_Roles { get; set; }
        IDbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }
    }
    public class DalContext : DbContext, IDalContext
    {
        public DalContext()
            : base("DefaultConnection")
        {
        }

        public DalContext(string nameOrConnectionString):base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new webpages_Roles.Configuration());
        }

        public void ExecuteTransaction(Action action)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public EnumValueModel FindEnumValue(string type, string name)
        {
            return EnumTypes
                .First(enumType => string.Compare(enumType.Type, type, StringComparison.OrdinalIgnoreCase) == 0).Values
                .First(enumValue => string.Compare(enumValue.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public virtual IDbSet<SeedModel> Seeding { get; set; }
        public virtual IDbSet<EnumTypeModel> EnumTypes { get; set; }
        public virtual IDbSet<EnumValueModel> EnumValues { get; set; }
        public virtual IDbSet<AccountModel> Accounts { get; set; }
        public virtual IDbSet<webpages_Membership> webpages_Membership { get; set; }
        public virtual IDbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual IDbSet<webpages_Roles> webpages_Roles { get; set; }
        public virtual IDbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }
    }
}
