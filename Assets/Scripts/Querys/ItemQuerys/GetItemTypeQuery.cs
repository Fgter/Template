using QFramework;
using System;
using UnityEngine;

class GetItemTypeQuery : AbstractQuery<Type>
{
    int id;
    public GetItemTypeQuery(int id)
    {
        this.id = id;
    }
    protected override Type OnDo()
    {
        switch (id)
        {
            case int id when (id > 0 && id < 1000)://是种子
                return typeof(SeedItem);

            case int id when (id > 1000 && id < 2000)://是植物
                return default;
            case int id when (id > 2000 && id < 4000)://是食物
                return typeof(FoodItem);
            case int id when (id > 10000)://是收获物
                return typeof(HarvestItem);

            default:
                Debug.LogError("id" + id + " 没有这号东西，请查询策划案");
                return default;
        }
    }
}
