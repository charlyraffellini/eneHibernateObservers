using System.Collections.Generic;
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

	public class DataMapperCollectionObserver : NHibernateObserver<ICollection<DataMapper>>, IPostCollectionUpdateEventListener, IPostCollectionRecreateEventListener
	{
		public void OnPostUpdateCollection(PostCollectionUpdateEvent @event)
		{
			var obj = @event;
		}

		protected override void HandleNotification(ICollection<DataMapper> entity)
		{
			var obj = entity;
		}

		public void OnPostRecreateCollection(PostCollectionRecreateEvent @event)
		{
			var obj = @event;
		}
	}
}