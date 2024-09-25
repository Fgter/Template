using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : UISelectableImageGroup.UISelectableImage
{
    [SerializeField]
    Image icon;
    [SerializeField]
    TextMeshProUGUI count;
    [SerializeField]
    Transform sellout;
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    TextMeshProUGUI price;

    public ShopItem info;
    public void SetItem(ShopItem item, int count)
    {
        this.info = item;
        this.icon.overrideSprite = ResLoader.LoadSprite(info.itemDefine.Icon);
        this.count.text = count.ToString();
        this.Name.text = info.itemDefine.Name;
        this.price.text = info.define.Price.ToString();
        sellout.gameObject.SetActive(count == 0);
    }
}
