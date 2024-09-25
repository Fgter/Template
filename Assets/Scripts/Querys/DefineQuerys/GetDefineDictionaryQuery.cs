using Models;
using QFramework;
using System;
using System.Reflection;
using UnityEngine;


/// <summary>
/// 获取DefineModel中对应类型的字典
/// </summary>
/// <typeparam name="T"></typeparam>
class GetDefineDictionaryQuery<T> : AbstractQuery<T> where T :class
{
    protected override T OnDo()
    {
        var model = this.GetModel<DefineModel>();
        Type type = model.GetType();
        FieldInfo[] fileds = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        foreach(var p in fileds)
        {
            if (p.FieldType.Equals(typeof(T)))
                return p.GetValue(model) as T;
        }
        Debug.LogError(string.Format("DefineModel中没有{0}类型的字段",typeof(T)));
        return default;
    }
}
