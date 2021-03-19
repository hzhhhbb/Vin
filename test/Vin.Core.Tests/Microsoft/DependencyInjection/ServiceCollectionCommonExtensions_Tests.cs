using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Vin.DependencyInjection;
using Xunit;

namespace Microsoft.DependencyInjection
{
    public class ServiceCollectionCommonExtensions_Tests
    {
        [Fact]
        public void IsAdded_Test()
        {
            var services = new ServiceCollection();
            services.AddType(typeof(MyService));
            services.IsAdded<MyService>().ShouldBeTrue();
        }

        [Fact]
        public void GetSingletonInstanceOrNull_Test()
        {
            var services = new ServiceCollection();
            var obj = new MyService();
            services.AddSingleton(obj);
            services.GetSingletonInstanceOrNull<MyService>().ShouldBe(obj);
            services.GetSingletonInstanceOrNull<ServiceCollectionCommonExtensions_Tests>().ShouldBeNull();
        }

        [Fact]
        public void GetSingletonInstance_Test()
        {
            var services = new ServiceCollection();
            var obj = new MyService();
            services.AddSingleton(obj);
            services.GetSingletonInstance<MyService>().ShouldBe(obj);
            Should.Throw(() => services.GetSingletonInstance<ServiceCollectionCommonExtensions_Tests>(), typeof(InvalidOperationException));
        }

        public class MyService : ISingletonDependency
        {
        }
    }
}