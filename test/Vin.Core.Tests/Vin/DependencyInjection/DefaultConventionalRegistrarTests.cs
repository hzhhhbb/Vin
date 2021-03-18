using Xunit;
using Vin.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Vin.DependencyInjection
{
    public class DefaultConventionalRegistrarTests
    {
        [Fact()]
        public void AddTypeTest()
        {
            var services=new ServiceCollection();
            services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
            services.AddType(typeof(TransientClass));
            services.AddType(typeof(ScopedClass));
            services.AddType(typeof(SingletonClass));

            services.ToList().ShouldContain(u=>u.ServiceType==typeof(TransientClass)&&u.Lifetime==ServiceLifetime.Transient);
            services.ToList().ShouldContain(u=>u.ServiceType==typeof(ScopedClass)&&u.Lifetime==ServiceLifetime.Scoped);
            services.ToList().ShouldContain(u=>u.ServiceType==typeof(SingletonClass)&&u.Lifetime==ServiceLifetime.Singleton);

        }

        [Fact()]
        public void AddTypesTest()
        {
            var services=new ServiceCollection();
            services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
            services.AddTypes(typeof(TransientClass),typeof(ScopedClass),typeof(SingletonClass));

            services.ToList().ShouldContain(u=>u.ServiceType==typeof(TransientClass)&&u.Lifetime==ServiceLifetime.Transient);
            services.ToList().ShouldContain(u=>u.ServiceType==typeof(ScopedClass)&&u.Lifetime==ServiceLifetime.Scoped);
            services.ToList().ShouldContain(u=>u.ServiceType==typeof(SingletonClass)&&u.Lifetime==ServiceLifetime.Singleton);

        }

        public class TransientClass:ITransientDependency
        {

        }
        public class SingletonClass:ISingletonDependency
        {

        }
        public class ScopedClass:IScopedDependency
        {

        }
    }
}