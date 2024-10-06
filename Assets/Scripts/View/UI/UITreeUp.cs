using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;

public class UITreeUp : UIWindowBase
{
    [SerializeField]
    Button btnOxygenUp;
    [SerializeField]
    Button btnNewTree;
    [SerializeField]
    Button btnMaxHpUp;
    [SerializeField]
    bool IsRoot;

    ExtraRootData data;
    public void Init(ExtraRootData data)
    {
        this.data = data;
    }
    private void Start()
    {
        btnOxygenUp.onClick.AddListener(OxygenUp);
        btnNewTree.onClick.AddListener(NewTree);
        btnMaxHpUp.onClick.AddListener(MaxHpUp);
    }

    private void MaxHpUp()
    {
        if (!this.SendCommand(new DecreaseGoldCommand(1)))
        {
            UIManager.instance.ShowTip("金币不够捏");
            Close();
            return;
        }

        if (IsRoot)
        {
            this.GetModel<RootModel>().maxHp += 10;
            this.GetModel<RootModel>().hp.Value += 10;
        }
        else
        {
            data.maxHp += 10;
            data.hp.Value += 10;
        }

    }

    private void NewTree()
    {
        Close();
        //if (!this.SendCommand(new DecreaseGoldCommand(1)))
        //{
        //    UIManager.instance.ShowTip("金币不够捏");
        //    return;
        //}
        GameObject prefab = ResLoader.Load<GameObject>("Prefabs/ExtraRootShow");
        GameObject go = GameObject.Instantiate(prefab);
        ExtraRootShow show = go.GetComponent<ExtraRootShow>();
        if(IsRoot)
        {
            show.startPos = RootMono.instance.transform.position;
            show.radius = this.GetModel<RootModel>().radius;
        }
        else
        {
            show.startPos = data.transform.position;
            show.radius = data.radius;
        }
    }

    private void OxygenUp()
    {
        if (!this.SendCommand(new DecreaseGoldCommand(1)))
        {
            UIManager.instance.ShowTip("金币不够捏");
            Close();
            return;
        }

        if (IsRoot)
            this.GetModel<RootModel>().diameter.Value += 1;
        else
            data.diameter.Value += 1;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
