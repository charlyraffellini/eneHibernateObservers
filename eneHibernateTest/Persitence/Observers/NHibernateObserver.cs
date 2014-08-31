using System;
using NHibernate.Event;

namespace eneHibernateTest.Persitence.Observers
{
	public abstract class NHibernateObserver<T> : INHibernateObserver where T : class
	{
		public void OnPostInsert(PostInsertEvent @event)
		{
			HandleNotification(@event);
		}

		public void OnPostUpdate(PostUpdateEvent @event)
		{
			HandleNotification(@event);
		}

		public void OnPostDelete(PostDeleteEvent @event)
		{
			HandleNotification(@event);
		}

		protected abstract void HandleNotification(T entity);

		private void HandleNotification(IPostDatabaseOperationEventArgs @event)
		{
			try
			{
				var entity = this.ConvertToEntity(@event);
				this.HandleNotification(entity);
			}
			catch (InvalidEntityTypeException) { }
		}

		private T ConvertToEntity(IPostDatabaseOperationEventArgs @event)
		{
			var entity = @event.Entity;
			var entityType = entity.GetType();

			if (entityType != typeof(T))
				throw new InvalidEntityTypeException();

			return entity as T;
		}
	}

	public interface INHibernateObserver { }

	public class InvalidEntityTypeException : ApplicationException
	{
	}
}