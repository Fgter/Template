using QFramework;
using UnityEngine;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

class EnemySpawner : MonoBehaviour, IController
{
    Timer timer = new Timer();
    EnemySpawnSystem system;
    Coroutine spawnCor;
    float interval;

    [SerializeField]
    GameObject enemyWarrior;
    [SerializeField]
    GameObject enemyAssassin;
    [SerializeField]
    GameObject enemyArcher;
    [SerializeField]
    GameObject enemyThrower;
    [SerializeField]//////////
    Text countdownText;
    private void Start()
    {
        system = this.GetSystem<EnemySpawnSystem>();
        this.RegisterEvent<RuleChanged>(v => OnRuleChanged(v.rule)).UnRegisterWhenGameObjectDestroyed(gameObject);
        system.StartSpawn();
    }

    private void OnRuleChanged(SpawnRuleDefine rule)
    {
        if (spawnCor != null)
            StopCoroutine(spawnCor);
        interval = rule.SpawnInterval;
        timer.Delay(5, () => StartSpawn());
    }

    private void StartSpawn()
    {
        spawnCor = StartCoroutine(SpawnIEnum());
    }

    IEnumerator SpawnIEnum()
    {
        List<int> enemies = new List<int>();
        while (true)
        {
            enemies = system.GetEnemies();
            if (enemies.Count <= 0)
                yield break;
            foreach (var enemy in enemies)
            {
                switch (enemy)
                {
                    case 1:
                        this.SendCommand(new CreateEnemyCommand(enemyWarrior));
                        break;
                    case 2:
                        this.SendCommand(new CreateEnemyCommand(enemyAssassin));
                        break;
                    case 3:
                        this.SendCommand(new CreateEnemyCommand(enemyArcher));
                        break;
                    case 4:
                        this.SendCommand(new CreateEnemyCommand(enemyThrower));
                        break;
                }
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private void Update()
    {
        if ((4.99 - timer.time) >= 0)
        {
            if (!countdownText.gameObject.activeSelf)
                countdownText.gameObject.SetActive(true);
            countdownText.text = (Mathf.Ceil(5-timer.time)).ToString();
        }
        else
             if (countdownText.gameObject.activeSelf)
            countdownText.gameObject.SetActive(false);
    }

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
}
