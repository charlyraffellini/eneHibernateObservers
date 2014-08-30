namespace eneHibernateTest.Models
{
	public class DataMapper : IdentificableObject
	{
		public virtual string Name { get; set; }

		public DataMapper() { }
		public DataMapper(string name)
		{
			this.Name = name;
		}
	}

}
