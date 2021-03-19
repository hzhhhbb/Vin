# �������
��Ϊ������ܵĺ��Ĳ��֣�������ṩ�˳��õ���չ��������������Զ�����ע������

## �Զ�����ע��
�������ڹٷ�������ע�������ʵ�֣����������ע�벻��Ϥ���ɲ��Ĺٷ��ĵ�[ASP.NET Core ����ע��](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0)

����ע�������Լ��ע����Ϊ������ע�뷽ʽ�����ṩ���Զ�����չע����������

### ����������������
```csharp
[Dependency(ServiceLifetime.Singleton)]
public class AppServices : IAppServices
{
}
or
public class AppServices : ITransientDependency
{
}
```

���У�ͨ�����Զ���ķ��������������ȼ�����ʹ�ýӿڶ������������

### ע�����ķ�ʽ
��ע��������ʱ�򣬿�ѡ�������������ǡ����������ע�����ַ�ʽ������
```csharp
[Dependency(ServiceLifetime.Singleton, RegisterType.Replace)]
public class BAppServices : IAppServices
{
}
```
`RegisterType`��ö�����ͣ�������ֵ����Ӧ����ע�뷽ʽ���ֱ���`Normal,TryAdd,Replace`

### ���屩¶�ķ���
ͨ������`ExposeServicesAttribute`��������Ҫ��¶�ķ���ӿڻ��߷�������

```csharp
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

[Fact]
public void Should_Get_ExposedServices_By_Conventional()
{
    var exposedServices = ExposedServiceExplorer.GetExposedServices(typeof(DefaultDerivedService));

    exposedServices.Count.ShouldBe(3);
    exposedServices.ShouldContain(typeof(DefaultDerivedService));
    exposedServices.ShouldContain(typeof(IService));
    exposedServices.ShouldContain(typeof(IDerivedService));
}
```

����ͼ��ʾ���������������ͱ�¶����`DefaultDerivedService`

������ڷ�����ʹ��`ExposeServicesAttribute`���ԣ�����������Լ����¶����
1. ��������
2. ͬ�������нӿڣ��� `AppServices: IAppServices: IServices`����ʹ��`IAppServices`��`IServices`��¶`AppServices`����

����ڷ�����ʹ��`ExposeServicesAttribute`���ԣ���¶�ӹ��캯������ķ��񣬲�������������`IncludeSameNameInterface`��`IncludeItself`�������Ƿ�¶���������ͬ���ӿ�

## ʹ�÷�ʽ

1.��ʼ���Զ�ע��ע����

```csharp
var services = new ServiceCollection();
services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
```

2. ʹ����չ����ע�뼴��

```csharp
services.AddAssemblyOf<CustomConventionalRegistrarTests>();
services.AddTypes(typeof(MyCustomClass), typeof(MyClass), typeof(MyNonRegisteredClass));
services.AddType<AppServices>();
```

## �Զ���ע����

1. ����ע����
```csharp
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
```

2. ���Զ�ע��ǰ�����ע������`ServiceCollection`��
```csharp
var services = new ServiceCollection();
services.AddConventionalRegistrar(new MyCustomConventionalRegistrar());
```