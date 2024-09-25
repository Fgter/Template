using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScenePlanting : MonoBehaviour
{
    [SerializeField]
    Button _btnBag;
    [SerializeField]
    Button _btnShop;

    private void Start()
    {
        _btnBag.onClick.AddListener(OpenBag);
        _btnShop.onClick.AddListener(OpenShop);
    }

    private void OpenShop()
    {
        UIManager.instance.Show<UIShop>(new UIShopData(1));
    }

    private void OpenBag()
    {
        UIManager.instance.Show<UIBag>(null);
    }


}
