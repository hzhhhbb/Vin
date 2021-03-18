using System;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Vin.DependencyInjection
{
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase
    {
        public override void AddType(IServiceCollection services, Type type)
        {
            if (this.IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            var dependencyAttribute = this.GetDependencyAttributeOrNull(type);
            var serviceLifetime = this.GetServiceLifeTimeOrNull(type, dependencyAttribute);
            if (serviceLifetime == null)
            {
                return;
            }

            var exposedServiceTypes = ExposedServiceExplorer.GetExposedServices(type);

            // TODO 支持过滤暴露的服务

            foreach (Type exposedServiceType in exposedServiceTypes)
            {
                var serviceDescriptor = this.CreateServiceDescriptor(exposedServiceType, type, serviceLifetime.Value);

                if (dependencyAttribute == null)
                {
                    services.Add(serviceDescriptor);
                }
                else
                {
                    switch (dependencyAttribute.RegisterType)
                    {
                        case RegisterType.Normal:
                            services.Add(serviceDescriptor);
                            break;
                        case RegisterType.Replace:
                            services.Replace(serviceDescriptor);
                            break;
                        case RegisterType.TryAdd:
                            services.TryAdd(serviceDescriptor);
                            break;
                        default:
                            throw new NotImplementedException($"未实现的注入方式：{nameof(dependencyAttribute.RegisterType)}");
                    }
                }
            }
        }

        protected virtual ServiceDescriptor CreateServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifeTime)
        {
            return ServiceDescriptor.Describe(serviceType, implementationType, lifeTime);
        }

        protected virtual DependencyAttribute GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        /// <summary>
        /// 根据服务定义的特性 <see cref="DependencyAttribute"/> ，确定服务的生命周期
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <param name="dependencyAttribute"><see cref="DependencyAttribute"/></param>
        /// <returns><see cref="ServiceLifetime"/></returns>
        protected virtual ServiceLifetime? GetServiceLifeTimeOrNull(Type type, DependencyAttribute dependencyAttribute)
        {
            return dependencyAttribute?.Lifetime ?? this.GetServiceLifetimeOrNullFromClassHierarchy(type);
        }

        /// <summary>
        /// 根据继承的接口，确定服务的生命周期
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns><see cref="ServiceLifetime"/></returns>
        protected virtual ServiceLifetime? GetServiceLifetimeOrNullFromClassHierarchy(Type type)
        {
            if (type.IsAssignableTo<ITransientDependency>())
            {
                return ServiceLifetime.Transient;
            }

            if (type.IsAssignableTo<IScopedDependency>())
            {
                return ServiceLifetime.Scoped;
            }

            if (type.IsAssignableTo<ISingletonDependency>())
            {
                return ServiceLifetime.Singleton;
            }

            return null;
        }
    }
}