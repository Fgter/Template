using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class RootAttackedCommand : AbstractCommand
{
    RootModel model;
    protected override void OnExecute()
    {
        model = this.GetModel<RootModel>();
        model.hp.Value = model.hp.Value - 1 > 0 ? model.hp.Value - 1 : 0;
        this.SendEvent(new RootAttackedEvent { changeValue = 1, currentValue = model.hp.Value });
    }
}
