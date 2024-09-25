using QFramework;
using System;
using Models;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

class GetDefineByType : AbstractQuery<dynamic>
{
    Type type;
    int id;
    public GetDefineByType(Type type, int id)
    {
        this.type = type;
        this.id = id;
    }

    protected override dynamic OnDo()
    {
        MethodInfo method = this.GetType().GetMethod("GetDefine", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(type);
        object define = method.Invoke(this, null);
        return Convert.ChangeType(define, type);
    }

    T GetDefine<T>()
    {
        DefineModel model = this.GetModel<DefineModel>();
        if (model.allDefines.TryGetValue(type, out dynamic dic))
        {
            Dictionary<int, T> defines = (dic as Dictionary<int, T>);
            if (defines.ContainsKey(id))
                return defines[id];
            else
            {
                Debug.LogError(string.Format("id:{0} 在{1}配置中不存在", id, typeof(T)));
                return default;
            }
        }
        else
        {
            Debug.LogError(typeof(T) + " 在allDefine配置中不存在");
            return default;
        }
    }
}
