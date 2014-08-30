using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eneHibernateTest.Models;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using Ninject.Infrastructure.Language;

namespace eneHibernateTest.Persitence
{
	static public class NHibernateMappings
	{
		public static void CreateMappings(MappingConfiguration mappingConfiguration)
		{
			var automap = AutoMap.AssemblyOf<IdentificableObject>(new OrmMappingConventions())
				.Conventions.AddFromAssemblyOf<OrmMappingConventions>()
				.UseOverridesFromAssemblyOf<OrmMappingConventions>()
				.OverrideAll(x => x.IgnoreProperties(property => !property.CanWrite));

			typeof(IdentificableObject)
				.GetSubtypes().Where(type => type.ShouldIncludeBase())
				.ForEach(type => automap.IncludeBase(type));

			var autoMappingContainer = mappingConfiguration.AutoMappings.Add(automap);
		}

		public static bool ShouldIncludeBase(this Type type)
		{
			return type.IsAbstract
				&& (type.IsSubclassOf(typeof(IdentificableObject)) || type.HasAttribute<IncludeBaseAttribute>());
		}

		public static IEnumerable<Type> GetSubtypes(this Type aType)
		{
			var types = aType.Assembly.GetTypes();
			return types.Where(type => type.IsSubclassOf(aType) || type.GetInterfaces().Contains(aType));
		}

		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (T item in collection)
				action(item);
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class IncludeBaseAttribute : Attribute { }
}