using eneHibernateTest.DI.Providers;
using eneHibernateTest.Persitence;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Ninject;

namespace eneHibernateTest.App_Start.DI
{
	public static class NinjectBindings
	{
		public static void ApplyTo(IKernel kernel)
		{
			var dbConfiguration = DBConfiguration.Instance;
			kernel.Bind<IPersistenceConfigurer>().ToConstant(dbConfiguration);
			kernel.Bind<NHibernate.Cfg.Configuration>().ToConstant(SessionSourceCreator.CreateNHibernateConfiguration(dbConfiguration));

			kernel.Bind<ISessionSource>().ToConstant(
				new SessionSourceCreator().CreateInstance(dbConfiguration)
			);
			kernel.Bind<ISession>()
				.ToMethod(x => x.Kernel.Get<ISessionSource>().CurrentSession);

		}
	}
}