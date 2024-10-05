using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using UnityEngine.UI;

public class RootMono : MonoSingleton<RootMono>, IController,IHurt
{
    RootModel model;
    RootSystem system;

    [SerializeField]
    Image hpBar;
    [SerializeField]
    Transform safeArea;
    [SerializeField]
    Transform unsafeArea;

    PlayerHurtCommand playerHurtCommand;
    private void Start()
    {
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

    public void Hurt(float damage)
    {
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
}
