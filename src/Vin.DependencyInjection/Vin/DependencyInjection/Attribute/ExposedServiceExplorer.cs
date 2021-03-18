using System;
using System.Collections.Generic;
using System.Linq;

namespace Vin.DependencyInjection
{
    public static class ExposedServiceExplorer
    {
        private static readonly ExposeServicesAttribute DefaultExposeServicesAttribute =
            new ExposeServicesAttribute
            {
                IncludeSameNameInterface = true,
                IncludeItself = true
            };

        /// <summary>
        /// 获取继承<see cref="IExposedServiceTypesProvider"/> 接口的特性，定义的暴露服务
        /// </summary>
        /// <param name="type">需要被注入的服务</param>
        /// <returns>需要暴露的服务</returns>
        public static HashSet<Type> GetExposedServices(Type type)
        {
            return type
                .GetCustomAttributes(true)
                .OfType<IExposedServiceTypesProvider>()
                .DefaultIfEmpty(DefaultExposeServicesAttribute)
                .SelectMany(p => p.GetExposedServiceTypes(type))
                .ToHashSet();
        }
    }
}