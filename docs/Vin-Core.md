# 核心组件
作为整个框架的核心部分，本组件提供了常用的扩展方法、帮助类和自动依赖注入能力

## 自动依赖注入
本功能在官方的依赖注入基础上实现，如果对依赖注入不熟悉，可查阅官方文档[ASP.NET Core 依赖注入](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0)

特性注入和命名约定注入作为基础的注入方式，并提供了自定义扩展注册器的能力

### 定义服务的生命周期
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

其中，通过特性定义的服务生命周期优先级大于使用接口定义的生命周期

### 注入服务的方式
在注入容器的时候，可选择是新增、覆盖、如果存在则不注入三种方式，如下
```csharp
[Dependency(ServiceLifetime.Singleton, RegisterType.Replace)]
public class BAppServices : IAppServices
{
}
```
`RegisterType`是枚举类型，有三个值，对应三种注入方式，分别是`Normal,TryAdd,Replace`

### 定义暴露的服务
通过特性`ExposeServicesAttribute`来定义需要暴露的服务接口或者服务自身

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

如上图所示，将会以三个类型暴露服务`DefaultDerivedService`

如果不在服务上使用`ExposeServicesAttribute`特性，将按照以下约定暴露服务
1. 服务自身
2. 同名的所有接口，如 `AppServices: IAppServices: IServices`，则使用`IAppServices`和`IServices`暴露`AppServices`服务

如果在服务上使用`ExposeServicesAttribute`特性，则暴露从构造函数传入的服务，并根据特性属性`IncludeSameNameInterface`、`IncludeItself`来决定是否暴露服务自身和同名接口

## 使用方式

1.初始化自动注入注册器

```csharp
var services = new ServiceCollection();
services.AddConventionalRegistrar(new DefaultConventionalRegistrar());
```

2. 使用扩展方法注入即可

```csharp
services.AddAssemblyOf<CustomConventionalRegistrarTests>();
services.AddTypes(typeof(MyCustomClass), typeof(MyClass), typeof(MyNonRegisteredClass));
services.AddType<AppServices>();
```

## 自定义注册器

1. 定义注册器
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

2. 在自动注入前，添加注册器到`ServiceCollection`中
```csharp
var services = new ServiceCollection();
services.AddConventionalRegistrar(new MyCustomConventionalRegistrar());
```