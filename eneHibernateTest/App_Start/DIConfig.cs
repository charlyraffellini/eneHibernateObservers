using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using eneHibernateTest.App_Start.DI;
using Ninject;
using Ninject.Activation.Blocks;
using Ninject.Parameters;
using Ninject.Web.Common;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace eneHibernateTest.App_Start
{
    public static class DIConfig
    {

	    public static void RegisterDI(HttpConfiguration config)
	    {
			var kernel = new StandardKernel();
			kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
			NinjectBindings.ApplyTo(kernel);
			config.DependencyResolver = new NinjectResolver(kernel);
	    }
    }

	public class NinjectResolver : IDependencyResolver
	{
		private IActivationBlock kernel;
		public NinjectResolver(IKernel kernel)
		{
			this.kernel = kernel.BeginBlock();
		}

		public void Dispose()
		{
			IDisposable disposable = kernel;
			if (disposable != null) disposable.Dispose();
			kernel = null;
		}

		public object GetService(Type serviceType)
		{
			var request = kernel.CreateRequest(serviceType, null, new Parameter[0], true, true);
			return kernel.Resolve(request).SingleOrDefault();
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			var request = kernel.CreateRequest(serviceType, null, new Parameter[0], true, true);
			return kernel.Resolve(request).ToList();
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}
	}
}
