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
    public GameObject enemyPrefab;
    public GameObject gameEndPanel;
    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }

    public void Test1()
    {
        this.SendCommand(new CreateEnemyCommand(enemyPrefab));
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
        this.GetModel<PlayerModel>().hp.Value = 3;
        this.GetModel<RootModel>().hp.Value = 3;
    }
}
