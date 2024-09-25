using Models;
using QFramework;
using UnityEngine;

public class RemoveItemCommand : AbstractCommand<bool>
{
    int id;
    int count;
    public RemoveItemCommand(int id,int count)
    {
        this.id = id;
        this.count = count;
    }
    protected override bool OnExecute()
    {
        ItemModel model = this.GetModel<ItemModel>();
        if (model.Items.TryGetValue(id, out Item item))
        {
            if (item.count >= count)
            {
                item.count -= count;
                //这里判断一下等于0的时候清除字典中对应的映射关系
                this.SendEvent(new ItemCountChangeEvent());
                return true;
            }
            return false;
        }
        else
        {
            Debug.LogError("id:" + id + " not in ItemModel");
            return false;
        }
        
    }
}
