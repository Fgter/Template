using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExtraRootMono : MonoBehaviour, IController,IHurt,IPointerClickHandler
{
    [SerializeField]
    Image hpBar;
    [SerializeField]
    Transform safeArea;
    [SerializeField]
    Transform unsafeArea;
    [SerializeField]
    UITreeUp uiUp;

    Animator anim;

    public ExtraRootData data { get; set; }


    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        data.hp.Register(OnHurt).UnRegisterWhenGameObjectDestroyed(gameObject);
        data.diameter.Register(OnRadiusChnage).UnRegisterWhenGameObjectDestroyed(gameObject);
        data.RegisterDieEvent(OnDie);
        uiUp.Init(data);
    }

    private void OnRadiusChnage(float value)
    {
        safeArea.localScale = new Vector3(value, value, value);
    }

    private void OnHurt(float currentHp)
    {
        hpBar.fillAmount = currentHp / data.maxHp;
    }

    public void Hurt(float damage, bool dir)
    {
        if (dir)
            anim.SetTrigger("HurtRight");
        else
            anim.SetTrigger("HurtLeft");
        data.Hurt(damage);
    }

    void OnDie()
    {
        this.SendCommand(new KillExtraRootCommand(data));
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiUp.gameObject.SetActive(true);
    }
}
