using System;
using System.Runtime.CompilerServices;
using FluentNHibernate.Automapping;
using Ninject.Infrastructure.Language;

namespace eneHibernateTest.Persitence
{
	/// <summary>
	/// Provides the conventions that FluentNHibernate uses to automatically create the mappings
	/// </summary>
	public class OrmMappingConventions : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			return
				type.IsPublic
				&& type.Namespace != null
				&& type.Namespace.StartsWith("eneHibernateTest.Models")
				&& !type.IsEnum
				&& !type.IsLambdaExpression()
				&& !type.ContainsGenericParameters;
		}

		public override bool IsComponent(Type type)
		{
			return type.HasAttribute<ComponentAttribute>();
		}

		public override bool IsDiscriminated(Type type)
		{
			return type.HasAttribute<TablePerHierarchyAttribute>();
		}
	}

	public static class TypeHelpers
	{
		public static bool IsLambdaExpression(this Type self)
		{
			return self.IsDefined(typeof(CompilerGeneratedAttribute), false);
		}
	}

	/// <summary>
	/// Instructs NHibernate to use the specified class as Component
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ComponentAttribute : Attribute{ }

	/// <summary>
	/// Instructs NHibernate to map subclasses of this class using Table Per Hierarchy strategy
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class TablePerHierarchyAttribute : Attribute { }

}