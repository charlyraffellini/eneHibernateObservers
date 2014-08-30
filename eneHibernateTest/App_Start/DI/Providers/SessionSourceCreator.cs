using eneHibernateTest.Persitence;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using ISessionSource = eneHibernateTest.Persitence.ISessionSource;
using SessionSource = eneHibernateTest.Persitence.SessionSource;

namespace eneHibernateTest.DI.Providers
{
	public class SessionSourceCreator
	{
		public ISessionSource CreateInstance(IPersistenceConfigurer dbConfiguration)
		{
			var nhibernateConfiguration = CreateNHibernateConfiguration(dbConfiguration);
			return CreateSessionSource(nhibernateConfiguration);
		}

		public static NHibernate.Cfg.Configuration CreateNHibernateConfiguration(IPersistenceConfigurer dbConfiguration)
		{
			return Fluently.Configure()
				.Database(dbConfiguration)
				.CurrentSessionContext("web")
				.Mappings(NHibernateMappings.CreateMappings)
				.ExposeConfiguration(BuildSchema)
				.BuildConfiguration();
		}

		private static void BuildSchema(Configuration cfg)
		{
			new SchemaUpdate(cfg).Execute(false, true);
		}

		private static ISessionSource CreateSessionSource(NHibernate.Cfg.Configuration nhibernateConfiguration)
		{
			return new SessionSource(nhibernateConfiguration);
		}
	}
}