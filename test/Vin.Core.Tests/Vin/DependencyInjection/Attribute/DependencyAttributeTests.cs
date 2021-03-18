using System.Linq;
using Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Vin.DependencyInjection
{
    public class DependencyAttributeTests
    {
        [Fact]
        public void Should_Cover_Exists_Lifetime()
        {
            var services = new ServiceCollection();
            services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
            services.AddType<AppServices>();

            // 应该覆盖通过接口约定的服务生命周期
            services.ToList().ShouldContain(u => u.ServiceType == typeof(IAppServices) && u.Lifetime == ServiceLifetime.Singleton && u.ImplementationType == typeof(AppServices));
        }

        [Fact]
        public void Should_Replace_Exists_Services()
        {
            var services = new ServiceCollection();
            services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
            services.AddType<AppServices>();
            services.AddType<BAppServices>();

            // 应该替换同一ServiceType的服务
            services.ToList().ShouldContain(u => u.ServiceType == typeof(IAppServices) && u.Lifetime == ServiceLifetime.Singleton && u.ImplementationType == typeof(BAppServices));
            services.ToList().ShouldNotContain(u => u.ServiceType == typeof(IAppServices) && u.Lifetime == ServiceLifetime.Singleton && u.ImplementationType == typeof(AppServices));
        }

        [Fact]
        public void Should_Not_Replace_Exists_Services()
        {
            var services = new ServiceCollection();
            services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
            services.AddType<AppServices>();
            services.AddType<CAppServices>();

            // 不应该替换同一ServiceType的服务
            services.ToList().ShouldContain(u => u.ServiceType == typeof(IAppServices) && u.Lifetime == ServiceLifetime.Singleton && u.ImplementationType == typeof(AppServices));
            services.ToList().ShouldNotContain(u => u.ServiceType == typeof(IAppServices) && u.Lifetime == ServiceLifetime.Singleton && u.ImplementationType == typeof(CAppServices));
        }

        [Dependency(ServiceLifetime.Singleton)]
        public class AppServices : IAppServices
        {
        }

        [Dependency(ServiceLifetime.Singleton, RegisterType.Replace)]
        public class BAppServices : IAppServices
        {
        }

        [Dependency(ServiceLifetime.Singleton, RegisterType.TryAdd)]
        public class CAppServices : IAppServices
        {
        }

        public interface IAppServices : ITransientDependency
        {
        }
    }
}