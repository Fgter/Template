using Define;
using QFramework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Models;

struct UIShopData : IUIData
{
    public int shopId { get; set; }
    public UIShopData(int shopId)
    {
        this.shopId = shopId;
    }
}
public class UIShop : UIWindowBase
{
    [SerializeField]
    TextMeshProUGUI description;
    [SerializeField]
    TextMeshProUGUI gold;

    [Space()]
    [SerializeField]
    GameObject _uiShopItem;
    UISelectableImageGroup _group = new UISelectableImageGroup();
    [SerializeField]
    UINumberControlBtn _numberBtns;
    [SerializeField]
    UIUnlimitedSilidePage _pages;

    Dictionary<Transform, UIShopItem> shopItems = new Dictionary<Transform, UIShopItem>();//存储每个UIShopItem
    Dictionary<int, List<ShopItem>> groupShopItems = new Dictionary<int, List<ShopItem>>();//存储每组ShopItem
    Dictionary<RectTransform, List<UIShopItem>> uiShopItems = new Dictionary<RectTransform, List<UIShopItem>>();//存储三页中的UIShopItem
    int index;//shopitems的组下标
    ShopItem currentSelectItem;
    int shopId;
    private void Awake()
    {
        for (int i = 0; i < 3; i++)//给每一页创建uishopitems的对象
        {
            uiShopItems[_pages.PageTransforms[i]] = new List<UIShopItem>();
            for (int j = 0; j < 4; j++)
            {
                GameObject go = Instantiate(_uiShopItem, _pages.PageTransforms[i]);
                UIShopItem uiShopItem = go.GetComponent<UIShopItem>();
                uiShopItems[_pages.PageTransforms[i]].Add(uiShopItem);
                _group.AddElement(uiShopItem);
                shopItems[go.transform] = uiShopItem;
            }
        }
        currentSelectItem = null;
        _group.RegisterSelectAction(OnSelect);
        _pages.RegisterLastPageFunc(OnLastPage);
        _pages.RegisterNextPageFunc(OnNextPage);
        _numberBtns.RegisterBtnConfirmFunc(Buy);
        this.GetModel<PlayerModel>().Gold.Register(OnGoldChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
        gold.text = this.SendQuery(new GetGoldQuery()).ToString();
    }

    private void OnGoldChanged(int value)
    {
        gold.text = value.ToString();
    }

    public override void OnShow(IUIData showData)
    {
        UIShopData data = (UIShopData)showData;
        this.shopId = data.shopId;
        var description = this.SendQuery(new GetDefineQuery<ShopDefine>(shopId)).Description;
        this.description.text = description;
        List<ShopItem> items = this.SendQuery(new GetShopItemsQuery(data.shopId));
        //给物品按4个一组分组
        GroupItems(items);
        Refresh(_pages.currentPage.rectTransform);
    }

    void Refresh(RectTransform rt)
    {
        int tempIndex = 0;
        for (; tempIndex < groupShopItems[index].Count; tempIndex++)
        {
            ShopItem item = groupShopItems[index][tempIndex];
            uiShopItems[rt][tempIndex].gameObject.SetActive(true);
            uiShopItems[rt][tempIndex].SetItem(item, item.count);
        }
        for (; tempIndex < 4; tempIndex++)
        {
            uiShopItems[rt][tempIndex].gameObject.SetActive(false);
        }
       
    }

    void Buy()
    {
        if (currentSelectItem == null)
            return;
        if(this.SendCommand(new BuyItemCommand(shopId, currentSelectItem.define.Id, _numberBtns.currentNumber)))
        {
            UIManager.instance.ShowTip(string.Format("获得 {0} x {1}",currentSelectItem.itemDefine.Name,_numberBtns.currentNumber));
            Refresh(_pages.currentPage.rectTransform);
            _numberBtns.SetMaxNumber(currentSelectItem.count);
        }
        else
        {
            UIManager.instance.ShowTip("好像没有足够的money哦");
        }
    }
    void OnSelect(Transform ts)
    {
       currentSelectItem = shopItems[ts].info;
        description.text = currentSelectItem.define.Description;
        _numberBtns.SetMaxNumber(currentSelectItem.count);
    }
    void OnNextPage(RectTransform rt)
    {
        index = LimitValue(++index, groupShopItems.Count - 1);
        Refresh(rt);
    }

    void OnLastPage(RectTransform rt)
    {
        index = LimitValue(--index, groupShopItems.Count - 1);
        Refresh(rt);
    }

    int LimitValue(int value, int maxValue)
    {
        if (maxValue + 1 == 0)
            return 0;
        return (value % (maxValue + 1) + (maxValue + 1)) % (maxValue + 1);
    }

    void GroupItems(List<ShopItem> items)
    {
        groupShopItems.Clear();
        int times = items.Count % 4 == 0 ? items.Count / 4 : (items.Count / 4) + 1;
        for (int i = 0; i < times; i++)
        {
            groupShopItems[i] = new List<ShopItem>();
            for (int j = 0; j < 4; j++)
            {
                if (index < items.Count)
                {
                    groupShopItems[i].Add(items[index]);
                    index++;
                }
            }
        }
        index = 0;
    }
}
