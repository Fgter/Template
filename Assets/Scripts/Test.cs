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

    public void AddGold()
    {
        this.SendCommand(new IncreaseGoldCommand(100));
    }

    public void OpenUIPlayerUp()
    {
        UIManager.instance.Show<UIPlayerUp>(null);
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
        ResetPlayer();
        ResetMap();
        this.GetSystem<GameSystem>().Restart();
    }

    public void ResetPlayer()
    {
        this.GetModel<PlayerModel>().attack = 10;
        this.GetModel<PlayerModel>().maxHp.Value = 30;
        this.GetModel<PlayerModel>().hp.Value = 30;
        this.GetModel<PlayerModel>().speed = 5;
    }

    public void ResetMap()
    {
        var model = this.GetModel<RootModel>();
        foreach (var root in model.extraRoots)
        {
            Destroy(root.transform.gameObject);
        }
        model.extraRoots.Clear();
        model.diameter.Value = 10;
        model.maxHp = 3;
        model.hp.Value = 3;

    }

    public void RecoverHp()
    {
        this.GetModel<PlayerModel>().hp.Value = this.GetModel<PlayerModel>().maxHp.Value;
        this.GetModel<RootModel>().hp.Value = this.GetModel<RootModel>().maxHp;
    }
}
