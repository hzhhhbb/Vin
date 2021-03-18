using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Vin.DependencyInjection;

namespace Microsoft.DependencyInjection
{
    public static class ServiceCollectionConventionalRegistrationExtensions
    {
        public static IServiceCollection AddConventionalRegistrar(this IServiceCollection services, IConventionalRegistrar registrar)
        {
            GetOrCreateRegistrars(services).Add(registrar);
            return services;
        }

        public static HashSet<IConventionalRegistrar> GetConventionalRegistrars(this IServiceCollection services)
        {
            return GetOrCreateRegistrars(services);
        }

        private static ConventionalRegistrarHashSet GetOrCreateRegistrars(IServiceCollection services)
        {
            var conventionalRegistrars = services.GetSingletonInstanceOrNull<ConventionalRegistrarHashSet>();
            if (conventionalRegistrars == null)
            {
                conventionalRegistrars = new ConventionalRegistrarHashSet {new DefaultConventionalRegistrar()};
                services.AddSingleton(conventionalRegistrars);
            }

            return conventionalRegistrars;
        }
        public static IServiceCollection AddAssemblyOf<T>(this IServiceCollection services)
        {
            return services.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static IServiceCollection AddAssembly(this IServiceCollection services, Assembly assembly)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddAssembly(services, assembly);
            }

            return services;
        }

        public static IServiceCollection AddTypes(this IServiceCollection services, params Type[] types)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddTypes(services, types);
            }

            return services;
        }

        public static IServiceCollection AddType<TType>(this IServiceCollection services)
        {
            return services.AddType(typeof(TType));
        }

        public static IServiceCollection AddType(this IServiceCollection services, Type type)
        {
            foreach (var registrar in services.GetConventionalRegistrars())
            {
                registrar.AddType(services, type);
            }

            return services;
        }
    }
}