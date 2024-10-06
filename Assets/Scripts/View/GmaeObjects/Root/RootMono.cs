using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RootMono : MonoSingleton<RootMono>, IController,IHurt,IPointerClickHandler
{
    RootModel model;
    RootSystem system;

    [SerializeField]
    Image hpBar;
    [SerializeField]
    Transform safeArea;
    [SerializeField]
    Transform unsafeArea;
    [SerializeField]
    UITreeUp uiUp;

    Animator anim;
    PlayerHurtCommand playerHurtCommand;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        model = this.GetModel<RootModel>();
        system = this.GetSystem<RootSystem>();
        model.hp.Register(OnHurt).UnRegisterWhenGameObjectDestroyed(gameObject);
        model.diameter.Register(OnRadiusChnage).UnRegisterWhenGameObjectDestroyed(gameObject);
        playerHurtCommand = new PlayerHurtCommand(0);
    }

    private void OnRadiusChnage(float value)
    {
        safeArea.localScale = new Vector3(value, value, value);
    }

    private void OnHurt(float currentHp)
    {
        hpBar.fillAmount = currentHp / model.maxHp;

    }


    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    public void Hurt(float damage,bool dir)
    {
        if (dir)
            anim.SetTrigger("HurtRight");
        else
            anim.SetTrigger("HurtLeft");
        this.SendCommand(new RootAttackedCommand());
    }

    private void Update()
    {
        if (!system.GetPlayerStatus())
        {
            float hurtValue = Time.deltaTime * 5;
            playerHurtCommand.value = hurtValue;
            this.SendCommand(playerHurtCommand);
        }
            
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiUp.gameObject.SetActive(true);
    }
}
