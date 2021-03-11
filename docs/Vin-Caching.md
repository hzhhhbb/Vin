## Vin.Caching
一个强类型的分布式缓存组件
### 使用前提
配置任一分布式缓存组件
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDistributedMemoryCache(options => { });
}
```
或者
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddStackExchangeRedisCache(options => { });
}
```
### 用法
在DI容器中注册
```c#

public void ConfigureServices(IServiceCollection services)
{
    services.AddDistributedCacheStrongNameSupport();
}

```
在构造函数或其他地方注入即可使用
```c#
private readonly IDistributedCache<Entity> _entityCache;

public AccountController(IDistributedCache<Entity> entityCache)
{
    this._entityCache = entityCache;
}
```
### 扩展
默认序列化、反序列化服务是类型`Utf8JsonDistributedCacheSerializer`，继承自接口`IDistributedCacheSerializer`

如需要替换默认序列化、反序列化服务，可继承`IDistributedCacheSerializer`并实现接口方法后，在DI容器中替换默认实现即可
