using System;

namespace Vin.DependencyInjection
{
    public interface IExposedServiceTypesProvider
    {
        Type[] GetExposedServiceTypes(Type targetType);
    }
}