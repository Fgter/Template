using QFramework;
using UnityEngine;

public class CreateExtraRootCommand : AbstractCommand
{
    RootModel model;
    Vector3 startPos;
    Vector3 spawnPos;
    public CreateExtraRootCommand(Vector3 startPos, Vector3 spawnPos)
    {
        this.startPos = startPos;
        this.spawnPos = spawnPos;
    }

    protected override void OnExecute()
    {
        model = this.GetModel<RootModel>();
        GameObject prefab = ResLoader.Load<GameObject>("Prefabs/ExtraRoot");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.position = spawnPos;

        ExtraRootData data = new ExtraRootData();
        data.Init(go.transform);

        go.GetComponent<ExtraRootMono>().data = data;
        model.AddExtraRoot(data);

        //Éú³ÉÊ÷¸ù
        LineRenderer lineRenderer = go.GetComponentInChildren<LineRenderer>();
        lineRenderer.SetPosition(0, startPos - new Vector3(0, 0.85f, 0));
        lineRenderer.SetPosition(1, spawnPos - new Vector3(0, 0.85f, 0));
    }
}
