using Models;
using QFramework;
using UnityEngine;

class GetItemCountQuery : AbstractQuery<int>
{
    int id;
    public GetItemCountQuery(int id)
    {
        this.id = id;
    }
    protected override int OnDo()
    {
        ItemModel model = this.GetModel<ItemModel>();
        if (model.Items.TryGetValue(id, out Item item))
        {
            return item.count;
        }
        else
        {
            Debug.LogError("id:" + id + " not in ItemModel");
            return -1;
        }
    }
}
