using Models;
using QFramework;
using System.Collections.Generic;
using UnityEngine;

class GetItemQuery<T> : AbstractQuery<T> where T : Item
{
    int id;
    public GetItemQuery(int id)
    {
        this.id = id;
    }

    protected override T OnDo()
    {
        ItemModel model = this.GetModel<ItemModel>();
        if (model.classifyItems.TryGetValue(typeof(T), out dynamic dic))
        {
            Dictionary<int, T> items = dic as Dictionary<int, T>;
            if (items.ContainsKey(id))
                return items[id];
            else
            {
                Debug.LogError(string.Format("Item:{0} 在{1}中不存在", id, typeof(T)));
                return default;
            }
        }
        else
        {
            Debug.LogError(typeof(T) + " 在classifyItems中不存在");
            return default;
        }
    }
}
