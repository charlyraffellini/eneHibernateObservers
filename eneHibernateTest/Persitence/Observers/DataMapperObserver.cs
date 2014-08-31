using eneHibernateTest.Models;
using NHibernate.Event;

namespace eneHibernateTest.Persitence.Observers
{
	public class DataMapperObserver : NHibernateObserver<DataMapper>, IPostUpdateEventListener
	{
		protected override void HandleNotification(DataMapper entity)
		{
			var cosa = entity;
		}
	}
}