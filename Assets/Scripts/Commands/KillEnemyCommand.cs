using QFramework;
using UnityEngine;
class KillEnemyCommand : AbstractCommand
{
    EnemyModel model;
    EnemyData data;
    public KillEnemyCommand(EnemyData data)
    {
        this.data = data;
    }
    protected override void OnExecute()
    {
        model = this.GetModel<EnemyModel>();
        model.RemoveEnemy(data);
        GameObject prefab = ResLoader.Load<GameObject>("Prefabs/Gold");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.position = data.transform.position;
        this.SendEvent(new EnemyDieEvent());
    }
}
