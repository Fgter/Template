using QFramework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : AbstractModel
{
    //public Dictionary<int, EnemyData> enemiesDict = new Dictionary<int, EnemyData>();
    public List<EnemyData> enemiesList = new List<EnemyData>();
    protected override void OnInit()
    {

    }

    public Transform GetEnemyByDistance(Transform player)
    {
        if (enemiesList.Count <= 0)
            return null;
        Transform result = enemiesList[0].transform;
        float minDistance = (enemiesList[0].transform.position - player.position).sqrMagnitude;
        foreach (var enemy in enemiesList)
        {
            float distance = (enemy.transform.position - player.position).sqrMagnitude;
            if (distance < minDistance)
            {
                result = enemy.transform;
                minDistance = (enemy.transform.position - player.position).sqrMagnitude;
            }
        }
        return result;
    }

    public void RemoveEnemy(EnemyData data)
    {
        if (enemiesList.Contains(data))
            enemiesList.Remove(data);
    }
}
