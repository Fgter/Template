using Models;
using QFramework;
using System.Collections.Generic;

class GetItemsQuery<T> : AbstractQuery<List<T>> where T : Item
{
    static List<T> result = new List<T>(20);
    protected override List<T> OnDo()
    {
        ItemModel model = this.GetModel<ItemModel>();
        result.Clear();
        if (model.classifyItems.TryGetValue(typeof(T), out dynamic dic))
        {
            Dictionary<int, T> items = dic as Dictionary<int, T>;
            foreach (var item in items.Values)
            {
                if (item.count <= 0)
                    continue;
                result.Add(item);
            }
            return result;
        }
        else
        {
            return result;
        }
    }
}
