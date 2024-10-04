using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class RootModel : AbstractModel
{
    public BindableProperty<int> hp { get; set; } = new BindableProperty<int>();
    protected override void OnInit()
    {
        hp.SetValueWithoutEvent(3);
    }

   
}
