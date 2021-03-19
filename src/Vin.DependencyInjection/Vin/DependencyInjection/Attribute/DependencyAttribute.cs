using System;
using Microsoft.Extensions.DependencyInjection;

namespace Vin.DependencyInjection
{
    /// <summary>
    /// 依赖注入特性
    /// </summary>
    public class DependencyAttribute:Attribute
    {
        public DependencyAttribute(ServiceLifetime lifetime,RegisterType registerType=DependencyInjection.RegisterType.Normal)
        {
            this.Lifetime = lifetime;
            this.RegisterType = registerType;
        }

        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime? Lifetime { get;}

        /// <summary>
        /// 注册方式
        /// </summary>
        public RegisterType RegisterType { get;}
    }
}