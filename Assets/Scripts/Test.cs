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
    public IArchitecture GetArchitecture()
    {
        return Template.Interface;
    }
    [ContextMenu("Test4")]
    public void Test4()
    {
        this.SendCommand(new IncreaseGoldCommand(500));
    }

    [ContextMenu("AddItems")]
    public void Test1()
    {
        this.SendCommand(new AddItemCommand(1, 3));
        this.SendCommand(new AddItemCommand(2, 3));
        this.SendCommand(new AddItemCommand(3, 3));
        this.SendCommand(new AddItemCommand(4, 3));
        this.SendCommand(new AddItemCommand(10001, 3));
        this.SendCommand(new AddItemCommand(10002, 3));
        this.SendCommand(new AddItemCommand(10003, 3));
        this.SendCommand(new AddItemCommand(10004, 3));
    }
}
