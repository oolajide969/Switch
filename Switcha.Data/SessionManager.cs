using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Switcha.Core.Mappings;
using Switcha.Core.Models;

namespace Switcha.Data
{
    public class SessionManager
    {

        const string connectionString = "Data Source =.; Initial Catalog = Switcha; Integrated Security = True";
        private static ISessionFactory factory;
        private static ISession session;
        public void CreateDatabase()
        {
            Fluently.Configure()
             .Database(MsSqlConfiguration.MsSql2012
                   .ConnectionString(connectionString)
                   .ShowSql())
              .Mappings(m =>
                m.FluentMappings.AddFromAssemblyOf<TransactionTypeMap>())
              .ExposeConfiguration(CreateSchema)
              .BuildSessionFactory();
        }

        public static void CreateSchema(Configuration config)
        {
            var schemaExport = new SchemaExport(config);
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
        }

        public static void ResetSchema(Configuration config)
        {
            config.SetProperty("current_session_context_class", "managed_web");
            new SchemaUpdate(config).Execute(false, true);
        }

        public void ResetDB()
        {
            Fluently.Configure()
               .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
               .Mappings(m => m.FluentMappings
               .AddFromAssemblyOf<TransactionTypeMap>())
               .ExposeConfiguration(ResetSchema)
               .BuildConfiguration();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings
            .AddFromAssemblyOf<TransactionTypeMap>())
              .ExposeConfiguration(ResetSchema)
               .BuildConfiguration()
            .BuildSessionFactory();
        }
        public static ISession Session()
        {
            if (factory == null)
            {
                factory = CreateSessionFactory();
            }
            if (session == null)
            {
                session = factory.OpenSession();
            }

            session.BeginTransaction();

            return session;
        }
    }
}
