using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Vin.DependencyInjection
{
    public class CustomConventionalRegistrarTests
    {
        [Fact]
        public void Should_Use_Custom_Conventions_If_Added()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.AddConventionalRegistrar(new MyCustomConventionalRegistrar());
            services.AddConventionalRegistrar(new MyCustomConventionalRegistrar1());
            services.AddTypes(typeof(MyCustomClass), typeof(MyClass), typeof(MyNonRegisteredClass));

            //Assert

            services.ToList().ShouldContain(u=>u.ServiceType==typeof(MyClass)&&u.Lifetime==ServiceLifetime.Transient);
            services.ToList().ShouldContain(u=>u.ServiceType==typeof(MyCustomClass)&&u.Lifetime==ServiceLifetime.Singleton);
            services.ToList().ShouldNotContain(u=>u.ServiceType==typeof(MyNonRegisteredClass)&&u.Lifetime==ServiceLifetime.Singleton);

        }

        public class MyCustomConventionalRegistrar : ConventionalRegistrarBase
        {
            public override void AddType(IServiceCollection services, Type type)
            {
                if (type == typeof(MyClass))
                {
                    services.AddSingleton<MyCustomClass>();
                }
            }
        }
        public class MyCustomConventionalRegistrar1 : ConventionalRegistrarBase
        {
            public override void AddType(IServiceCollection services, Type type)
            {
            }
        }

        public class MyCustomClass
        {
        }

        public class MyNonRegisteredClass
        {

        }

        public class MyClass : ITransientDependency
        {
            
        }
    }

  
}
