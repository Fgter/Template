using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Models;

public class Template : Architecture<Template>
{
    protected override void Init()
    {
        ResKit.Init();
        //Utility
        RegisterUtility(new Storage());
        //Model
        RegisterModel(new DefineModel());
        RegisterModel(new TimeModel());
        RegisterModel(new PlayerSettingModel());
        RegisterModel(new ItemModel());
        RegisterModel(new PlayerModel());
        //RegisterModel(new ShopModel());
        //System
        RegisterSystem(new TimeSystem());
        RegisterSystem(new ShopSystem());

        //GiveIntinalMaterials();
    }

    
}
