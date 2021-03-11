using Vin.Caching;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DistributedServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedCacheStrongName(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedCacheSerializer, Utf8JsonDistributedCacheSerializer>();
            services.AddSingleton(typeof(IDistributedCache< >), typeof(DistributedCache< >));
            services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));
            return services;
        }
    }
}