using System;
using System.Linq;
using eneHibernateTest.App_Start.DI;
using eneHibernateTest.Models;
using eneHibernateTest.Persitence;
using FluentAssertions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using Ninject;
using Xunit;

namespace eneHibernateTest.Test
{
	public class NHibernateConfigTest : IUseFixture<RealApiFixture>
	{
		private StandardKernel kernel;

		[Fact]
		public void CreateDBConTutti()
		{
			var getCreateOne = new Action(() => kernel.Get<NHibernate.Cfg.Configuration>());
			getCreateOne.ShouldNotThrow();
		}

		[Fact]
		public void CanCreateASession()
		{
			ISession session = null;
			var getCreateOne = new Action(() => session = kernel.Get<ISessionSource>().CreateSession());
			getCreateOne.ShouldNotThrow();
		}

		[Fact]
		public void CanGetAllObjectsFromDBAlthoughItHasNoOne()
		{
			var session = kernel.Get<ISessionSource>().CreateSession();
			var objects = session.Query<DataMapper>();
			objects.Should().BeEmpty();
		}

		[Fact]
		public void CanCreateAnObjectInDB()
		{
			var getCreateOne = new Action(() => kernel.Get<ISessionSource>().CreateSession().Save(new DataMapper("Jose mapper")));
			getCreateOne.ShouldNotThrow();
		}

		[Fact]
		public void JoseMapperEsDeVerdad()
		{
			var session = kernel.Get<ISessionSource>().CreateSession();

			var jose = session.Query<DataMapper>().First();
			jose.Name.Should().Be("Jose mapper");
		}

		[Fact]
		public void WhenModify()
		{
			var session = kernel.Get<ISessionSource>().CreateSession();
			session.Save(new DataMapper("Sri Lanka"));
			var obj = session.Query<DataMapper>().Where(d => d.Name == "Sri Lanka").First();
			obj.Name = obj.Name + "s";
			session.Update(obj);
			session.Flush();
		}

		[Fact]
		public void CanGetAllObjectsFromDB()
		{
			var session = kernel.Get<ISessionSource>().CreateSession();

			var objects = session.Query<DataMapper>();
			objects.Should().HaveCount(1);
		}

		public void SetFixture(RealApiFixture data)
		{
			this.kernel = data.Kernel;
		}
	}

	public class RealApiFixture
	{
		public RealApiFixture()
		{
			Kernel = new StandardKernel();
			NinjectBindings.ApplyTo(this.Kernel);
		}

		public StandardKernel Kernel { get; private set; }
	}

}
