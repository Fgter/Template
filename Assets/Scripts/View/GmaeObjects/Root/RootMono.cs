using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

public class RootMono : MonoSingleton<RootMono>, IController,IHurt
{
    RootModel model;

    [SerializeField]
    SpriteRenderer hp;
    [SerializeField]
    Vector3[] positions;
    [SerializeField]
    float[] fill;
    private void Start()
    {
        model = this.GetModel<RootModel>();
        model.hp.Register(OnHurt).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnHurt(int value)
    {
       if(value==3)
        {
            hp.transform.localPosition = positions[0];
            hp.size = new Vector2(1, fill[0]);
        }
       else if(value==2)
        {
            hp.transform.localPosition = positions[1];
            hp.size = new Vector2(1, fill[1]);
        }
        else if (value == 1)
        {
            hp.transform.localPosition = positions[2];
            hp.size = new Vector2(1, fill[2]);
        }
        else if (value == 0)
        {
            hp.transform.localPosition = positions[3];
            hp.size = new Vector2(1, fill[3]);
        }
    }


    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    public void Hurt(int damage)
    {
        this.SendCommand(new RootAttackedCommand());
    }
}
