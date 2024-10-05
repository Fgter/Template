using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class RootSystem : AbstractSystem
{
    RootModel model;
    protected override void OnInit()
    {
        model = this.GetModel<RootModel>();
    }

    public bool GetPlayerStatus()
    {
        Vector3 playerPos = PlayerMono.instance.transform.position;
        if (playerPos.magnitude <= (model.radius))
            return true;
        foreach (var root in model.extraRoots)
        {
            if (Vector3.Distance(root.postion, playerPos) <= root.radius)
                return true;
        }
        return false;
    }
}
