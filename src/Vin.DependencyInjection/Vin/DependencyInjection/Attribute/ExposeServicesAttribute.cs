using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vin.DependencyInjection
{
    /// <summary>
    /// 暴露服务特性
    /// </summary>
    public class ExposeServicesAttribute:Attribute, IExposedServiceTypesProvider
    {
        public Type[] ServicesTypes { get;}

        /// <summary>
        /// 暴露同名接口
        /// </summary>
        public bool IncludeSameNameInterface { get; set; }

        /// <summary>
        /// 暴露自身
        /// </summary>
        public bool IncludeItself { get; set; }

        public ExposeServicesAttribute(params Type[] types)
        {
            this.ServicesTypes = types ?? new Type[0];
        }

        public Type[] GetExposedServiceTypes(Type targetType)
        {
            var servicesList = this.ServicesTypes.ToList();

            if (this.IncludeSameNameInterface)
            {
                var sameNameInterfaceList = this.GetSameNameInterfaceServices(targetType);
                foreach (Type interfacesType in sameNameInterfaceList)
                {
                    servicesList.AddIfNotContains(interfacesType);
                }
            }

            if (this.IncludeItself)
            {
                servicesList.AddIfNotContains(targetType);
            }

            return servicesList.ToArray();
        }

        /// <summary>
        /// 获取同名接口的服务，如 AppServices：IAppServices(or AppServices)，则使用IAppServices暴露AppServices服务
        /// </summary>
        /// <param name="type"> <see cref="Type"/> </param>
        /// <returns></returns>
        protected virtual List<Type> GetSameNameInterfaceServices(Type type)
        {
            var serviceTypes = new List<Type>();

            // Type and TypeInfo https://docs.microsoft.com/en-us/dotnet/api/system.reflection.typeinfo?redirectedfrom=MSDN&view=net-5.0
            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                var interfaceName = interfaceType.Name;

                if (interfaceName.StartsWith("I"))
                {
                    interfaceName = interfaceName.Right(interfaceName.Length - 1);
                }

                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfaceType);
                }
            }
            return serviceTypes;
        }
    }
}
