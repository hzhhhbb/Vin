using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Vin.Caching.Tests.Vin.Caching
{
    public class TestBase
    {
        protected IServiceProvider ServiceProvider;

        public TestBase()
        {
           var hostBuilder= Host.CreateDefaultBuilder(null);
           hostBuilder.ConfigureServices(services =>
           {
               services.AddLogging();
               services.Configure<DistributedCacheEntryOptions>(options =>
               {
                   options.AbsoluteExpiration = new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0));
                   options.SlidingExpiration = new TimeSpan(0, 0, 30, 0);
               });

               services.AddDistributedMemoryCache();
               services.AddDistributedCacheStrongName();
           });
           var IHost = hostBuilder.Build();
           this.ServiceProvider = IHost.Services;

        }
    }
}
