using Define;

public class ShopItem
{
    public ShopItemDefine define { get; set; }
    public IIconItemDefine itemDefine { get; set; }
    public int count { get; set; }
    public bool status { get; set; }

    public ShopItem(ShopItemDefine define,IIconItemDefine itemDefine,int count)
    {
        this.define = define;
        this.itemDefine = itemDefine;
        this.count = count;
        this.status = define.Status;
    }
}
