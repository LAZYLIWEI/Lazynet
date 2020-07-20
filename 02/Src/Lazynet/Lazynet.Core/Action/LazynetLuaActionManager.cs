/*
* ==============================================================================
*
* Filename:LazynetLuaActionManager
* Description: 
*
* Version: 1.0
* Created: 2020/7/20 23:27:36
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lazynet.Core.Action
{
    /// <summary>
    /// lua action manager
    /// </summary>
    public class LazynetLuaActionManager
    {
        public Dictionary<string, ILazynetAction> ActionDict { get; }

        public LazynetLuaActionManager()
        {
            this.ActionDict = new Dictionary<string, ILazynetAction>();
        }

        public ILazynetAction Get(string key)
        {
            var action = this.ActionDict[key];
            return action;
        }

        public void Add(string key, ILazynetAction action)
        {
            if (!this.ActionDict.ContainsKey(key))
            {
                this.ActionDict.Add(key, action);
            }
        }

        public void AddArray(LuaTable actionTable)
        {
            if (actionTable == null)
            {
                return;
            }
            foreach (var item in actionTable)
            {
                string key = item.Key.ToString();
                this.ActionDict.Add(key, new LazynetLuaAction(key, actionTable));
            }
        }

        public void AddArray(object entity, Func<string, string, string> formatKey)
        {
            var entityType = entity.GetType();
            var methodArray = entityType.GetMethods();
            foreach (var item in methodArray)
            {
                var attr = item.GetCustomAttribute<LazynetSharpActionAttribute>();
                if (attr == null)
                {
                    continue;
                }
                string key = string.Format("{0}_{1}", entityType.Name, item.Name);
                if (formatKey != null)
                {
                    key = formatKey(entityType.Name, item.Name);
                }
                this.ActionDict.Add(key, new LazynetSharpAction(entity, item));
            }
        }

        public bool Remove(string key)
        {
            return this.ActionDict.Remove(key);
        }

        public object[] Call(string key, object[] parameterArray)
        {
            if (!this.ActionDict.ContainsKey(key))
            {
                return null;
            }
            var action = this.ActionDict[key];
            var result = action.Call(parameterArray);
            return result;
        }
    }
}
