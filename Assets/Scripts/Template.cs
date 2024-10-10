using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Models;

public class PirateBomb : Architecture<PirateBomb>
{
    protected override void Init()
    {
        ResKit.Init();
        //Utility
        RegisterUtility(new Storage());
        //Model
        RegisterModel(new DefineModel());
        RegisterModel(new TimeModel());
        RegisterModel(new ItemModel());
        RegisterModel(new PlayerModel());
        RegisterModel(new ShopModel());
        //System
        RegisterSystem(new TimeSystem());
        RegisterSystem(new ShopSystem());

        //GiveIntinalMaterials();
    }

    
}
