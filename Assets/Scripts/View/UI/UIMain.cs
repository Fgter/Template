using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

public class UIMain : MonoBehaviour,IController
{
    [SerializeField]
    Image hp;
    [SerializeField]
    Text gold;
    [SerializeField]
    Text maxHP;

    PlayerModel model;

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    private void Start()
    {
        model = this.GetModel<PlayerModel>();
        model.maxHp.Register(OnMaxHpChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
        model.hp.Register(OnHpChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
        model.gold.Register(OnGoldChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
        gold.text = model.gold.Value.ToString();
        maxHP.text = model.maxHp.Value.ToString();
    }

    private void OnMaxHpChanged(float obj)
    {
        maxHP.text = obj.ToString();
    }

    private void OnGoldChanged(int obj)
    {
        gold.text = obj.ToString();
    }

    private void OnHpChanged(float obj)
    {
        hp.fillAmount = obj / model.maxHp.Value;
    }
}
