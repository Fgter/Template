using QFramework;
using System.Collections.Generic;
using Models;
using UnityEngine;

class GetDefinesQuery<T> : AbstractQuery<List<T>>
{
    static List<T> result = new List<T>(20);
    protected override List<T> OnDo()
    {
        DefineModel model = this.GetModel<DefineModel>();
        result.Clear();
        if (model.allDefines.TryGetValue(typeof(T), out dynamic dic))
        {
            Dictionary<int, T> defines = (dic as Dictionary<int, T>);
            result.AddRange(defines.Values);
            return result;
        }
        else
        {
            Debug.LogError(typeof(T) + "配置表不存在");
            return result;
        }
    }
}
