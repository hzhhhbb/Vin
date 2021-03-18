using System;
using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Vin.Caching.Tests.Vin.Caching
{
    public class DistributedCacheConfigureOptionsTest : TestBase
    {
        [Fact]
        public void Configure_CacheOptions()
        {
            var cacheOptions = this.ServiceProvider.GetRequiredService<IOptions<DistributedCacheEntryOptions>>();
            cacheOptions.ShouldNotBeNull();
            cacheOptions.Value.AbsoluteExpiration.ShouldBe(new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0)));
            cacheOptions.Value.SlidingExpiration.ShouldBe(new TimeSpan(0, 0, 30, 0));
        }

        [Fact]
        public void Expired()
        {
            var cache = this.ServiceProvider.GetRequiredService<IDistributedCache<Entity>>();
            DistributedCacheEntryOptions options= new DistributedCacheEntryOptions();
            var entity = new Entity
            {
                Id = 1,
                Name = "Vincent"
            };

            {
                //滑动过期
                options.SlidingExpiration=new TimeSpan(0,0,0,1);
                cache.Set("4",entity,options);
                Thread.Sleep(1500);
                cache.Get("4").ShouldBeNull();
            }
            {
                // 相对当前时间 过期
                options=new DistributedCacheEntryOptions();
                options.AbsoluteExpirationRelativeToNow=new TimeSpan(0,0,0,1);
                cache.Set("5",entity,options);

                Thread.Sleep(1500);
                cache.Get("5").ShouldBeNull();
            }
            {
                //绝对时间过期
                options=new DistributedCacheEntryOptions();
                options.AbsoluteExpiration=new DateTimeOffset(DateTime.Now.AddSeconds(1));
                cache.Set("6",entity,options);

                Thread.Sleep(1500);
                cache.Get("6").ShouldBeNull();
            }
        }

        [Fact]
        public async void Set_Get_Remove_Cache_ItemAsync()
        {
            var cache = ServiceProvider.GetRequiredService<IDistributedCache<Entity>>();
            var entity = new Entity
            {
                Id = 1,
                Name = "Vincent"
            };

            await Should.NotThrowAsync(cache.SetAsync("1", entity));

            var cachedEntity = await cache.GetAsync("1");

            cachedEntity.Id.ShouldBe(entity.Id);
            cachedEntity.Name.ShouldBe(entity.Name);

            await Should.NotThrowAsync(cache.RemoveAsync("1"));

            var shouldNull = await cache.GetAsync("1");
            shouldNull.ShouldBeNull();
        }

        [Fact]
        public void Set_Get_Remove_Cache_Item()
        {
            var cache = ServiceProvider.GetRequiredService<IDistributedCache<Entity>>();
            var entity = new Entity
            {
                Id = 1,
                Name = "Vincent"
            };

            Should.NotThrow(() =>
            {
                cache.Set("1", entity);
            });

            var cachedEntity = cache.Get("1");

            cachedEntity.Id.ShouldBe(entity.Id);
            cachedEntity.Name.ShouldBe(entity.Name);

            Should.NotThrow(() =>
            {
                cache.Remove("1");
            });

            var shouldNull = cache.Get("1");
            shouldNull.ShouldBeNull();
        }

        [Fact]
        public void Set_Null_Item()
        {
            var cache = ServiceProvider.GetRequiredService<IDistributedCache<Entity>>();
            Entity entity = null;
            Should.NotThrow(()=>
            {
                 cache.Set("2", entity);
            });
        }

        [Fact]
        public async void Refresh()
        {
            var cache = ServiceProvider.GetRequiredService<IDistributedCache<Entity>>();
            var entity = new Entity
            {
                Id = 1,
                Name = "Vincent"
            };

            DistributedCacheEntryOptions options= new DistributedCacheEntryOptions();
            options.SlidingExpiration=new TimeSpan(0,0,0,1);
            cache.Set("3",entity,options);

            Should.NotThrow(()=>
            {
                cache.Refresh("3");
            });

          await  Should.NotThrowAsync( cache.RefreshAsync("3"));

        }

        public class Entity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}