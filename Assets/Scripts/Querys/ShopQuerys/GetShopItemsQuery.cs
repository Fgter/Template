using System.Collections.Generic;
using QFramework;
using Models;
using UnityEngine;

class GetShopItemsQuery : AbstractQuery<List<ShopItem>>
{
    int id;
    static List<ShopItem> result = new List<ShopItem>(20);
    public GetShopItemsQuery(int shopId)
    {
        this.id = shopId;
    }
    protected override List<ShopItem> OnDo()
    {
        ShopModel model = this.GetModel<ShopModel>();
        result.Clear();
        if (model.shopItemDict.ContainsKey(id))
        {
            result.AddRange(model.shopItemDict[id].Values);
            return result;
        }
        Debug.LogError(string.Format("ShopModel中没有找到id为{0}的商店", id));
        return result;
    }
}
