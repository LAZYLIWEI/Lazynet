/*
* ==============================================================================
*
* Filename: ReflectionHelper
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 9:45:42
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Core.Util
{
    public static class ReflectionHelper
    {
        public static Type[] GetTypes(this Assembly ass)
        {
            var types = ass.GetTypes();
            return types;
        }

        public static bool IsSubclass(this Type subClassType, Type parentClassType)
        {
            return subClassType.IsSubclassOf(parentClassType);
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

        public static T CreateInstance<T>(this Type type) where T : class
        {
            var obj = System.Activator.CreateInstance(type);
            if (obj is null)
            {
                return default(T);
            }
            return obj as T;
        }

        public static T CreateInstance<T>(this Type type, params object[] args) where T : class
        {
            var obj = System.Activator.CreateInstance(type, args);
            if (obj is null)
            {
                return default(T);
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

        /// <summary>
        /// 该是否继承而来
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static object IsInherit(this MethodInfo methodInfo)
        {
            return methodInfo.GetBaseDefinition().DeclaringType == methodInfo.DeclaringType;
        }

        public static T GetCustomAttribute<T>(this Type entity) where T: Attribute
        {
            var attr = entity.GetCustomAttribute(entity);
            return attr as T;
        }

    }
}
