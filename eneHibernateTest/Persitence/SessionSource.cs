using NHibernate;

namespace eneHibernateTest.Persitence
{
	public class SessionSource : ISessionSource
	{
		public SessionSource(NHibernate.Cfg.Configuration dbConfiguration)
		{
			this.ValidateSchema(dbConfiguration);
			this.SessionFactory = dbConfiguration.BuildSessionFactory();
		}

		public ISessionFactory SessionFactory { get; set; }

		public ISession CreateSession()
		{
			var session = OpenSession();
			return session;
		}

		//TODO: ESTO VA A ROMPER PORQUE NO ESTA CONFIGURADO EL WEBAPI MODULE PARA QUE CREE Y BINDEE UNA SESSION AL CurrentSessionContext DE NHIBERNATE
		//TODO: EN ESTE CASO EL CurrentSessionContext ES WEB Y ESTA MANEJADO POR NHIBERNATE. ES EL STRING "web" QUE ESTA HARDCODEADO EN LA CONFIGURATION.
		public ISession CurrentSession
		{
			get
			{
				return this.SessionFactory.GetCurrentSession();
			}
		}

		protected virtual ISession OpenSession()
		{
			return this.SessionFactory.OpenSession();
		}

		protected virtual void ValidateSchema(NHibernate.Cfg.Configuration dbConfiguration) { }
	}

	public interface ISessionSource
	{
		ISession CreateSession();
		ISession CurrentSession { get; }
		ISessionFactory SessionFactory { get; }
	}
}