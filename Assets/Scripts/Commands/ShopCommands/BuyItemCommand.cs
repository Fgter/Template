using Models;
using QFramework;

class BuyItemCommand : AbstractCommand<bool>
{
    int shopId;
    int shopItemId;
    int count;
    public BuyItemCommand(int shopId, int shopItemId, int count)
    {
        this.shopId = shopId;
        this.shopItemId = shopItemId;
        this.count = count;
    }
    protected override bool OnExecute()
    {
        var shopItem = this.GetModel<ShopModel>().shopItemDict[shopId][shopItemId];
        int price = shopItem.define.Price * count;
        if (shopItem.count < count)
            return false;
        if (this.SendQuery(new GetGoldQuery()) < price)
            return false;
        shopItem.count -= count;
        this.SendCommand(new DecreaseGoldCommand(price));
        this.SendCommand(new AddItemCommand(shopItem.define.ItemId, count));
        return true;
    }
}
