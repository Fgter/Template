using System.Collections.Generic;
using QFramework;
using Models;
using System.Reflection;

public class AddItemCommand : AbstractCommand
{
    int id;
    int count;
    public AddItemCommand(int id, int count)
    {
        this.id = id;
        this.count = count;
    }

    protected override void OnExecute()
    {
        ItemModel model = this.GetModel<ItemModel>();
        if (model.Items.TryGetValue(id, out Item item))
        {
            item.count += count;
        }
        else
        {
            Item newItem = this.SendCommand(new CreateItemCommand(id));
            newItem.count += count;
            model.Items.Add(id, newItem);
            MethodInfo method = this.GetType().GetMethod("AddToClassifyItems").MakeGenericMethod(newItem.Type);
            method.Invoke(this,new object[] { model,newItem});
        }
        this.SendEvent(new ItemCountChangeEvent());
    }

    public void AddToClassifyItems<T>(ItemModel model,T newItem)
    {
        if (model.classifyItems.TryGetValue(typeof(T), out dynamic dic))
        {
            Dictionary<int, T> items = dic as Dictionary<int, T>;
            items[id] = newItem;
        }
        else
        {
            Dictionary<int, T> newItems = new Dictionary<int, T>();
            newItems[id] = newItem;
            model.classifyItems[typeof(T)] = newItems;
        }
       
    }
}
