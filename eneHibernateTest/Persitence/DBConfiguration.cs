using System;
using FluentNHibernate.Cfg.Db;

namespace eneHibernateTest.Persitence
{
	public static class DBConfiguration
	{
		public static IPersistenceConfigurer Instance
		{
			get
			{
				return MySQLConfiguration
					.Standard
					.ConnectionString(ConnectionString);
			}
		}

		public static string ConnectionString
		{
			get
			{
				Func<int, string> makeConnectionStringForPort = port =>
					string.Format("Server=localhost;Port={0};Database=enehibernate;Uid=parsimotion;Pwd=parsimotion;", port);

				return makeConnectionStringForPort(3306);
			}
		}
	}
}