using System;
using System.Reflection;

namespace WeiXin.Utilitys
{
    public sealed class ReflectionHelper
    {
        public static T GetCustomAttribute<T>(Enum source) where T : Attribute
        {
            Type sourceType = source.GetType();
            string sourceName = Enum.GetName(sourceType, source);
            FieldInfo field = sourceType.GetField(sourceName);
            object[] attributes = field.GetCustomAttributes(typeof(T), false);
            foreach (object attribute in attributes)
            {
                if (attribute is T)
                    return (T)attribute;
            }
            return null;
        }

        public static object CreateObject(string assemblyString, string typeName)
        {
            return Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}
