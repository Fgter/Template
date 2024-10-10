using QFramework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UIBag : UIWindowBase
{
    [SerializeField]
    GameObject bagItemPrefab;
    [SerializeField]
    Transform content;
    [SerializeField]
    UIButtonGroup btnGroup;

    List<UIBagItem> bagItems = new List<UIBagItem>();
    Type currentItemType;
    private void Start()
    {
        this.RegisterEvent<ItemCountChangeEvent>(v => RefreshCurrentPage()).UnRegisterWhenGameObjectDestroyed(this);
    }

    public override void OnShow(IUIData showData)
    {
        btnGroup.ActiveInitialSelectedBtn();
        //RefreshHarvest();
    }

    //public void RefreshHarvest()
    //{
    //    Refresh<HarvestItem>();
    //    currentItemType = typeof(HarvestItem);
    //}

    //public void RefreshSeed()
    //{
    //    Refresh<SeedItem>();
    //    currentItemType = typeof(SeedItem);
    //}

    //public void RefreshFood()
    //{
    //    Refresh<FoodItem>();
    //    currentItemType = typeof(FoodItem);
    //}

    //public void RefreshSpecialty()
    //{
    //    Refresh<SpecialtyItem>();
    //    currentItemType = typeof(SpecialtyItem);
    //}

    void Refresh<T>() where T : Item
    {
        List<T> items = this.SendQuery(new GetItemsQuery<T>());
        bool bagItemEnough = bagItems.Count >= items.Count;
        if (bagItemEnough)
        {
            int temp = 0;
            for (; temp < items.Count; temp++)
            {
                bagItems[temp].SetItem(items[temp], items[temp].count);
                bagItems[temp].gameObject.SetActive(true);

            }
            for (; temp < bagItems.Count; temp++)
            {
                bagItems[temp].gameObject.SetActive(false);
            }
        }
        else
        {
            int temp = 0;
            for (; temp < bagItems.Count; temp++)
            {
                bagItems[temp].SetItem(items[temp], items[temp].count);
            }
            for (; temp < items.Count; temp++)
            {
                CreateBagItem(items[temp], items[temp].count);
            }
        }
    }

    void RefreshCurrentPage()
    {
        MethodInfo method = this.GetType().GetMethod("Refresh",BindingFlags.Instance | BindingFlags.NonPublic).
                            MakeGenericMethod(currentItemType);
        method.Invoke(this, null);
    }

    void CreateBagItem(Item item, int count)
    {
        GameObject go = GameObject.Instantiate(bagItemPrefab, content);
        UIBagItem bagItem = go.GetComponent<UIBagItem>();
        bagItem.SetItem(item, count);
        bagItems.Add(bagItem);
    }
}
