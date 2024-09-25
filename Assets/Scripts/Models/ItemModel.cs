using System.Collections.Generic;
using QFramework;
using System;
using SaveData;
using System.Reflection;

namespace Models
{
    public class ItemModel : AbstractModel
    {
        public Dictionary<int, Item> Items = new Dictionary<int, Item>();//保存所有Item，方便遍历及更改数量

        public Dictionary<Type, dynamic> classifyItems = new Dictionary<Type, dynamic>();//分类保存Item，方便分类遍历

        protected override void OnInit()
        {
            Load();
            CommonMono.AddQuitAction(Save);
        }

        void Load()
        {
            BagSaveData saveData = this.GetUtility<Storage>().Load<BagSaveData>();
            if (saveData.items == null)
                return;
            foreach (var data in saveData.items)
            {
                Type type = this.SendQuery(new GetItemTypeQuery(data.id));
                MethodInfo method = this.GetType().GetMethod("CreateAndCollectItem", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(type);
                method.Invoke(this, new object[] { data.id, data.count });
            }
        }

        void Save()
        {
            BagSaveData saveData = new BagSaveData();
            saveData.items = new List<ItemSaveData>();
            foreach (var item in Items)
            {
                saveData.items.Add(new ItemSaveData(item.Key, item.Value.count));
            }
            this.GetUtility<Storage>().Save(saveData);
        }

        /// <summary>
        /// 创建Item对象并且加到字典中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="count"></param>
        void CreateAndCollectItem<T>(int id, int count) where T : Item
        {
            Type itemType = typeof(T);
            Type defineType = typeof(T).GetProperty("define").PropertyType;
            dynamic define = this.SendQuery(new GetDefineByType(defineType, id));
            T item = Activator.CreateInstance(itemType, new object[] { define }) as T;
            item.count = count;
            Items.Add(id, item);

            //collect
            if (classifyItems.TryGetValue(typeof(T), out dynamic dic))
            {
                Dictionary<int, T> items = dic as Dictionary<int, T>;
                items[id] = item;
            }
            else
            {
                Dictionary<int, T> newItems = new Dictionary<int, T>();
                newItems[id] = item;
                classifyItems[typeof(T)] = newItems;
            }
        }
    }
}