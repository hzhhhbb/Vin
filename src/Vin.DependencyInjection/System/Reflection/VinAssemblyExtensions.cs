namespace System.Reflection
{
    public static class VinAssemblyExtensions
    {
        public static Type[] GetTypesIgnoreException(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }
    }
}