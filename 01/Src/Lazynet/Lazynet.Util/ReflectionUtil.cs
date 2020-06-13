using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Util
{
    /// <summary>
    /// 相关反射功能帮助类
    /// </summary>
    public static class ReflectionUtil
    {
        public static Type[] GetTypes(this Assembly ass)
        {
            var types = ass.GetTypes();
            return types;
        }

        public static object CreateInstance(string assemblyPath, string className)
        {
            var assembly = Assembly.Load(assemblyPath);
            return assembly.CreateInstance(className);
        }

        public static object CreateInstance(this Type type)
        {
            var obj = System.Activator.CreateInstance(type);
            return obj;
        }

        public static object CreateInstance(this Type type, params object[] args)
        {
            var obj = System.Activator.CreateInstance(type, args);
            return obj;
        }

        public static T CreateInstance<T>(this Type type) where T:class
        {
            var obj = System.Activator.CreateInstance(type);
            if (obj is null)
            {
                return default;
            }
            return obj as T;
        }

        public static T CreateInstance<T>(this Type type, params object[] args) where T : class
        {
            var obj = System.Activator.CreateInstance(type, args);
            if (obj is null)
            {
                return default;
            }
            return obj as T;
        }

        public static object CallMethod(this object obj, 
            string name,
            object[] parameters)
        {
            Type t = obj.GetType();
            MethodInfo mt = t.GetMethod(name);
            if (mt != null)
            {
                return mt.Invoke(obj, parameters);
            }
            else
            {
                return null;
            }
        }

        public static object CallMethod(this MethodInfo methodInfo, 
            object obj,
            object[] parameters)
        {
            return methodInfo?.Invoke(obj, parameters);
        }

    }
}
