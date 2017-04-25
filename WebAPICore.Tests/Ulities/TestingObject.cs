using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace WebAPICore.Tests.Ulities
{
    public class TestingObject<T> where T : class
    {
        private Dictionary<Type, object> DependencyMap { get; } = new Dictionary<Type, object>();

        public void AddDependency<TDependency>(TDependency dependency)
        {
            this.DependencyMap.Add(typeof(TDependency), dependency);
        }

        public TDependency GetDependency<TDependency>() where TDependency : class
        {
            Type type = typeof(TDependency);

            if (!this.DependencyMap.TryGetValue(type, out object dependency))
            {
                throw new Exception($"Testing object doesn't contain dependency of type {type}.");
            }

            return dependency as TDependency;
        }

        public T GetResolvedTestingObject()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            foreach (var dependency in this.DependencyMap)
            {
                TypeInfo typeInfo = dependency.Key.GetTypeInfo();

                if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Mock<>))
                {
                    PropertyInfo propertyInfo = dependency.Key.GetProperty("Object",
                        BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    object value = propertyInfo.GetValue(dependency.Value);

                    serviceCollection.AddSingleton(dependency.Key.GenericTypeArguments[0], value);
                }
                else
                {
                    serviceCollection.AddSingleton(dependency.Key, dependency.Value);
                }
            }

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            return ActivatorUtilities.CreateInstance<T>(serviceProvider);
        }
    }
}