using QFramework;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : AbstractSystem
{
    protected override void OnInit()
    {
        this.GetModel<RootModel>().hp.Register(OnRootHurt);
        this.GetModel<PlayerModel>().hp.Register(OnRootHurt);
    }

    private void OnRootHurt(float value)
    {
        if (value <= 0)
        {
            this.SendEvent(new GameEnd());
            Time.timeScale = 0;
        }
    }



    public void Restart()
    {
        this.GetModel<EnemyModel>().enemiesList.Clear();
        Time.timeScale = 1;
        SceneManager.LoadScene("Fgter");
    }
}
