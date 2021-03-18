using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Vin.DependencyInjection
{
    public abstract class ConventionalRegistrarBase : IConventionalRegistrar
    {
        public void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypesIgnoreException().Where(type => type != null && type.IsClass && !type.IsAbstract && !type.IsGenericType).ToArray();
            this.AddTypes(services, types);
        }

        public void AddTypes(IServiceCollection services, params Type[] types)
        {
            foreach (Type type in types)
            {
                this.AddType(services,type);
            }
        }

        protected virtual bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        public abstract void AddType(IServiceCollection services, Type type);

    }
}