using Xunit;
using Shouldly;

namespace Vin.DependencyInjection
{
    public class ExposedServiceExplorerTests
    {
        [Fact()]
        public void Should_Get_Only_One_ExposedServices()
        {
            // Act
            var exposedServices=ExposedServiceExplorer.GetExposedServices(typeof(ExplicitDerivedService));

            // Assert
            exposedServices.Count.ShouldBe(1);
            exposedServices.ShouldContain(typeof(IDerivedService));
        }

        [Fact()]
        public void Should_Get_Two_ExposedServices()
        {
            var exposedServices=ExposedServiceExplorer.GetExposedServices(typeof(AppServices));

            exposedServices.Count.ShouldBe(2);
            exposedServices.ShouldContain(typeof(AppServices));
            exposedServices.ShouldContain(typeof(IAppServices));

        }

        [Fact]
        public void Should_Get_ExposedServices_By_Conventional()
        {
            var exposedServices = ExposedServiceExplorer.GetExposedServices(typeof(DefaultDerivedService));

            exposedServices.Count.ShouldBe(3);
            exposedServices.ShouldContain(typeof(DefaultDerivedService));
            exposedServices.ShouldContain(typeof(IService));
            exposedServices.ShouldContain(typeof(IDerivedService));
        }

        public class DefaultDerivedService : IDerivedService
        {
        }

        [ExposeServices(typeof(IService))]
        public interface IDerivedService : IService
        {
        }

        public interface IService
        {
        }

        [ExposeServices(typeof(IDerivedService))]
        public class ExplicitDerivedService : IDerivedService
        {

        }

        public class AppServices:IAppServices
        {

        }
        public interface IAppServices
        {

        }

    }
}