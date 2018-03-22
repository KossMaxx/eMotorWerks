using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Dal;
using log4net;
using Libraries.Core.Backend.Authorization;
using Logs;
using Microsoft.Practices.ObjectBuilder2;
using Project.Kernel;

namespace DataGenerator
{
    public interface IDataLoader
    {
        void Seed();
        void ExecuteAction(Action action, string nameAction);
        ISaContext SaContext { get; set; }
        IDalContext DalContext { get; set; }
        ILogsContext LogsContext { get; set; }
        IWrapper<ILog> Log { get; set; }
        IIdentityRepository IdentityRepository { get; set; }
    }

    public class DataLoader:IDataLoader
    {
        public DataLoader(ISaContext saContext, IDalContext dalContext, ILogsContext logsContext, IWrapper<ILog> log, IIdentityRepository identityRepository)
        {
            SaContext = saContext;
            DalContext = dalContext;
            LogsContext = logsContext;
            Log = log;
            IdentityRepository = identityRepository;
        }

        public void RecreateDb()
        {
            var instanceSqlServer = ConfigurationManager.AppSettings["InstanceSqlServer"];
            var folderSqlServer = ConfigurationManager.AppSettings["folderSqlServer"];
            var workSqlDbName = ConfigurationManager.AppSettings["WorkSqlDbName"];
            var logsSqlDbName = ConfigurationManager.AppSettings["LogsSqlDbName"];
            var sqlDbUser = ConfigurationManager.AppSettings["SqlDbUser"];

            ExecuteDeploymentScripts("RecreateDb.sql", instanceSqlServer, folderSqlServer, workSqlDbName, sqlDbUser);
            ExecuteDeploymentScripts("RecreateDb.sql", instanceSqlServer, folderSqlServer, logsSqlDbName, sqlDbUser);
        }

        public void WorkDbMigrations()
        {
            var workSqlDbName = ConfigurationManager.AppSettings["WorkSqlDbName"];
            ExecuteDeploymentScripts("WorkDbMigrations.sql", workSqlDbName);
        }
        public void LogsDbMigrations()
        {
            var logsSqlDbName = ConfigurationManager.AppSettings["LogsSqlDbName"];
            ExecuteDeploymentScripts("LogsMigrations.sql", logsSqlDbName);
        }
        public void ExecuteDeploymentScripts(string templateSqlFileName, params string[] args)
        {
            var pathToFolderSqlScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlScripts");
            var pathToTemplateFileScript = Path.Combine(pathToFolderSqlScript, templateSqlFileName);
            var sqlTemplateScript = File.ReadAllText(pathToTemplateFileScript);
            var sqlScript = string.Format(sqlTemplateScript, args);
            SaContext.ExecuteServerScript(sqlScript);
        }

        public void ExecuteDeploymentScripts()
        {
            ExecuteAction(RecreateDb, nameof(RecreateDb));
            ExecuteAction(WorkDbMigrations, nameof(WorkDbMigrations));
            ExecuteAction(LogsDbMigrations, nameof(LogsDbMigrations));
        }

        public void Seed()
        {
            ExecuteAction(ExecuteDeploymentScripts, nameof(ExecuteDeploymentScripts));

            ExecuteAction(InitIdentity, nameof(InitIdentity));
            ExecuteAction(CreateRolesAndUsersForTask, nameof(CreateRolesAndUsersForTask));

            ExecuteAction(UpdateSeedDal, nameof(UpdateSeedDal));
            ExecuteAction(UpdateSeedLogs, nameof(UpdateSeedLogs));
        }

        public void UpdateSeedDal()
        {
            DalContext.ExecuteTransaction(() =>
            {
                DalContext.Seeding.Add(new Dal.SeedModel { IsSeed = true });
                DalContext.SaveChanges();
            });
        }
        public void UpdateSeedLogs()
        {
            LogsContext.ExecuteTransaction(() =>
            {
                LogsContext.Seeding.Add(new Logs.SeedModel { IsSeed = true });
                LogsContext.SaveChanges();
            });
        }
        public void ExecuteAction(Action action, string nameAction)
        {
            Log.Instance.Info($"{nameAction} begin...");
            action();
            Log.Instance.Info($"{nameAction} end...");
        }

        protected void CreateRolesAndUsersForTask()
        {
            var roles = new []{"Employees", "USA", "Managers", "Ukraine", "Developers", "Main Office"};
            roles.ForEach(role => IdentityRepository.AddRole(role));
            IdentityRepository.AddChildrenRoles("Employees", new List<string>{ "USA", "Managers", "Ukraine", "Developers" });
            IdentityRepository.AddChildrenRoles("Ukraine", new List<string> { "Main Office" });
            IdentityRepository.AddChildrenRoles("Developers", new List<string> { "Main Office" });

            CreateUserForTask("O. Cole", "o.cole@emotorwerks.com");
            CreateUserForTask("J. Shane", "j.shane@emotorwerks.com");
            CreateUserForTask("V. Petrov", "v.petrov@emotorwerks.com");
            CreateUserForTask("M. Popov", "m.popov@emotorwerks.com");

            IdentityRepository.AddUserInRoles("O. Cole", new []{ "USA" });
            IdentityRepository.AddUserInRoles("J. Shane", new[] { "USA", "Managers" });
            IdentityRepository.AddUserInRoles("V. Petrov", new[] { "Main Office" });
            IdentityRepository.AddUserInRoles("M. Popov", new[] { "Main Office" });
        }

        private void CreateUserForTask(string userName, string email)
        {
            var account = new AccountModel
            {
                IsActivate = true,
                AccountName = userName,
                Email = email,
            };
            IdentityRepository.CreateUser(account,"!@#$%^&*()");
        }

        protected void InitIdentity()
        {
            IdentityRepository.Initialize();
            IdentityRepository.CreateDefaultRoles();
            IdentityRepository.CreateDefaultAdministrator();
        }

        public IDalContext DalContext { get; set; }
        public ILogsContext LogsContext { get; set; }
        public ISaContext SaContext { get; set; }
        public IWrapper<ILog> Log { get; set; }
        public IIdentityRepository IdentityRepository { get; set; }
    }
}
