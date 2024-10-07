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
    public GameObject gameWinPanel;
    public GameObject gmPanel;
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
        this.RegisterEvent<GameEndEvent>(OnGameEnd).UnRegisterWhenGameObjectDestroyed(gameObject);
        this.RegisterEvent<GameWinEvent>(OnGameWin).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void OnGameEnd(GameEndEvent obj)
    {
        gameEndPanel.SetActive(true);
    }

    private void OnGameWin(GameWinEvent obj)
    {
        gameWinPanel.SetActive(true);
    }

    public void Restart()
    {
        ResetPlayer();
        ResetMap();
        ResetEnemySpawn();
        this.GetSystem<GameSystem>().Restart();
    }

    void ResetEnemySpawn()
    {
        this.GetSystem<EnemySpawnSystem>().ResetParams();
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

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            gmPanel.SetActive(!gmPanel.activeSelf);
        }
    }
}
