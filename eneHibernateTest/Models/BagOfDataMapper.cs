using System.Collections.Generic;

namespace eneHibernateTest.Models
{
	public class BagOfDataMapper : IdentificableObject
	{
		public virtual ICollection<DataMapper> DataMappers { get; set; }

		public BagOfDataMapper()
		{
			this.DataMappers = new List<DataMapper>();
		}
	}
}