## Vin.Caching
һ��ǿ���͵ķֲ�ʽ�������
### ʹ��ǰ��
������һ�ֲ�ʽ�������
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddDistributedMemoryCache(options => { });
}
```
����
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddStackExchangeRedisCache(options => { });
}
```
### �÷�
��DI������ע��
```c#

public void ConfigureServices(IServiceCollection services)
{
    services.AddDistributedCacheStrongNameSupport();
}

```
�ڹ��캯���������ط�ע�뼴��ʹ��
```c#
private readonly IDistributedCache<Entity> _entityCache;

public AccountController(IDistributedCache<Entity> entityCache)
{
    this._entityCache = entityCache;
}
```
### ��չ
Ĭ�����л��������л�����������`Utf8JsonDistributedCacheSerializer`���̳��Խӿ�`IDistributedCacheSerializer`

����Ҫ�滻Ĭ�����л��������л����񣬿ɼ̳�`IDistributedCacheSerializer`��ʵ�ֽӿڷ�������DI�������滻Ĭ��ʵ�ּ���
