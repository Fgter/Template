using QFramework;
using UnityEngine;

class CreateEnemyCommand : AbstractCommand
{
    GameObject prefab;
    EnemyModel model;
    public CreateEnemyCommand(GameObject prefab)
    {
        this.prefab = prefab;
    }
    protected override void OnExecute()
    {
        model = this.GetModel<EnemyModel>();
        EnemyData data = new EnemyData();
       
        GameObject go = GameObject.Instantiate(prefab);
        EnemyMonoBase mono = go.GetComponent<EnemyMonoBase>();
        data.Init(this.SendQuery(new GetDefineQuery<EnemyDefine>(mono.defineId)));
        mono.data = data;
        go.transform.position = RandomPos();
        data.transform = go.transform;
        model.enemiesList.Add(data);
    }

    Vector3 RandomPos()
    {
        float x;
        float y;
        do
        {
            x = Random.Range(-10, 10);
            y = Random.Range(-10, 10);
        }
        while ((x > -5 && x < 5) || (y > -5 && y < 5));



        return new Vector3(x, y, 0);
    }
}
