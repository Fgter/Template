using QFramework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : AbstractSystem
{
    List<SpawnRuleDefine> rules = new List<SpawnRuleDefine>();

    int enemyCount;
    int aliveEnemyCount;
    SpawnRuleDefine currentRule;
    List<int> currentEnemies = new List<int>();
    bool win;
    protected override void OnInit()
    {
        rules = this.SendQuery(new GetDefinesQuery<SpawnRuleDefine>());
        this.RegisterEvent<EnemyDieEvent>(OnEnemyDie);
    }

    private void OnEnemyDie(EnemyDieEvent obj)
    {
        aliveEnemyCount--;
        if (aliveEnemyCount <= 0)
            RefreshEnemies();
    }

    public void ResetParams()
    {
        aliveEnemyCount = 0;
        rules = this.SendQuery(new GetDefinesQuery<SpawnRuleDefine>());
        currentEnemies.Clear();
        currentRule = default;
        enemyCount = 0;
        win = false;
    }
    public List<int> GetEnemies()
    {
        int enemyCount = currentEnemies.Count;
        int spawnCount = currentRule.EachSpawnCount;
        List<int> result = new List<int>();
        if (enemyCount > 0)
        {
            if (enemyCount >= spawnCount)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    result.Add(currentEnemies[i]);
                }
                for (int i = 0; i <result.Count; i++)
                {
                    currentEnemies.Remove(result[i]);
                }
            }
            else
            {
                foreach(var enemy in currentEnemies)
                {
                    result.Add(enemy);
                }
                currentEnemies.Clear();
            }
        }
        return result;
    }

    public void StartSpawn()
    {
        RefreshEnemies();
    }

    void RefreshRule()
    {
        if (rules.Count>0)
        {
            currentRule = rules[0];
            rules.RemoveAt(0);
            enemyCount = currentRule.Enemy1Count + currentRule.Enemy2Count + currentRule.Enemy3Count + currentRule.Enemy4Count;
            aliveEnemyCount = enemyCount;
            this.SendEvent(new RuleChanged() { rule = currentRule });
        }
        else
        {
            this.SendEvent(new GameWinEvent());
            win = true;
        }

    }

    void RefreshEnemies()
    {
        if (win)
            return;
        RefreshRule();
        currentEnemies.Clear();
        currentEnemies.Capacity = enemyCount;
        for (int i = 0; i < currentRule.Enemy1Count; i++)
        {
            currentEnemies.Add(1);
        }
        for (int i = 0; i < currentRule.Enemy2Count; i++)
        {
            currentEnemies.Add(2);
        }
        for (int i = 0; i < currentRule.Enemy3Count; i++)
        {
            currentEnemies.Add(3);
        }
        for (int i = 0; i < currentRule.Enemy4Count; i++)
        {
            currentEnemies.Add(4);
        }
        Shuffle(currentEnemies);
    }

    void Shuffle(List<int> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, count);

            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
