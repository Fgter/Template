using System.Collections.Generic;
using QFramework;
using Models;
using UnityEngine;
using Define;

class GetShopItemQuery : AbstractQuery<ShopItem>
{
    int shopId;
    int shopItemId;
    public GetShopItemQuery(int shopId, int shopItemId)
    {
        this.shopId = shopId;
        this.shopItemId = shopItemId;
    }
    protected override ShopItem OnDo()
    {
        ShopModel model = this.GetModel<ShopModel>();
        if (model.shopItemDict.ContainsKey(shopId))
        {
            if (model.shopItemDict[shopId].ContainsKey(shopItemId))
            {
                return model.shopItemDict[shopId][shopItemId];
            }
            else
            {
                Debug.LogError(string.Format("ShopModel:  shopItem:{0} 在shopId{1}中找不到", shopItemId, shopId));
                return default;
            }
        }
        else
        {
            Debug.LogError(string.Format("ShopModel:  shopId:{0}找不到", shopId));
            return default;
        }
    }
}
