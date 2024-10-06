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
            if (Vector3.Distance(root.transform.position, playerPos) <= root.radius)
                return true;
        }
        return false;
    }

    public Transform GetNearestRoot(Transform ts)
    {
        Transform result = RootMono.instance.transform;
        float minDistance = (ts.transform.position - result.position).sqrMagnitude;
        List<ExtraRootData> roots = model.extraRoots;
        foreach(var root in roots)
        {
            float distance = (ts.transform.position - root.transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                if (root.transform != null)
                    result = root.transform;
            }
        }
        return result;
    }
}
