using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

public class ExtraRootData
{
    Action DieEvent;
    public Transform transform { get; set; }
    public float maxHp { get; set; }
    public BindableProperty<float> hp { get; set; } = new BindableProperty<float>();
    public BindableProperty<float> diameter { get; set; } = new BindableProperty<float>();
    public float radius { get => diameter.Value / 2; }

    public void RegisterDieEvent(Action func) => DieEvent += func;
    public void UnRegisterDieEvent(Action func) => DieEvent -= func;

    public void Init(Transform transform)
    {
        this.transform = transform;
        maxHp = 3;
        hp.Value = 3;
        diameter.Value = 10;
    }

    public void Hurt(float damage)
    {
        if (hp.Value > damage)
            hp.Value -= damage;
        else
        {
            hp.Value = 0;
            DieEvent?.Invoke();
        }
    }
}
