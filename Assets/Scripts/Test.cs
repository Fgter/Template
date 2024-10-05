using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Models;
using Define;
using SaveData;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Test : ViewController, IController
{
    public GameObject enemyWarrior;
    public GameObject enemyAssassin;
    public GameObject enemyArcher;
    public GameObject enemyThrower;
    public GameObject gameEndPanel;
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    public void Test1()
    {
        this.SendCommand(new CreateEnemyCommand(enemyWarrior));
    }

    public void Test2()
    {
        this.SendCommand(new CreateEnemyCommand(enemyAssassin));
    }
    public void Test3()
    {
        this.SendCommand(new CreateEnemyCommand(enemyArcher));
    }
    public void Test4()
    {
        this.SendCommand(new CreateEnemyCommand(enemyThrower));
    }

    private void Start()
    {
        this.RegisterEvent<GameEnd>(OnGameEnd).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnGameEnd(GameEnd obj)
    {
        gameEndPanel.SetActive(true);
    }

    public void Restart()
    {
        this.GetSystem<GameSystem>().Restart();
    }

    public void RecoverHp()
    {
        this.GetModel<PlayerModel>().hp.Value = 30;
        this.GetModel<RootModel>().hp.Value = 3;
    }
}
