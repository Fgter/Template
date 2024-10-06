using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class RootModel : AbstractModel
{

    public float maxHp { get; set; }
    public BindableProperty<float> hp { get; set; } = new BindableProperty<float>();
    public BindableProperty<float> diameter { get; set; } = new BindableProperty<float>();//Ö±¾¶
    public float radius { get => diameter.Value / 2; }//°ë¾¶

    public List<ExtraRootData> extraRoots = new List<ExtraRootData>();
    protected override void OnInit()
    {
        maxHp = 3;
        hp.SetValueWithoutEvent(maxHp);
        diameter.Value = 10;
    }

    public void AddExtraRoot(ExtraRootData data)
    {
        extraRoots.Add(data);
    }

    public void RemoveExtraRoot(ExtraRootData data)
    {
        if (extraRoots.Contains(data))
            extraRoots.Remove(data);
    }
}
