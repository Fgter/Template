using QFramework;
using Models;
using SaveData;
using System.Collections.Generic;
using Define;
using System;
public class ShopSystem : AbstractSystem
{
    ShopModel _model;
    TimeSystem _timeSystem;
    
    protected override void OnInit()
    {
        _model = this.GetModel<ShopModel>();
        TimeSystem.RegisterClockUpdateAction(RefeshCount);
    }

    /// <summary>
    /// 游戏运行时检测时间,刷新数量
    /// </summary>
    /// <param name="time"></param>
    void RefeshCount(DateTime time)
    {
        foreach (var shop in _model.shopItemDict)
        {
            if (time.Equals(_model.shopDefines[shop.Key].RefreshTime))
            {
                foreach (var item in shop.Value)
                {
                    item.Value.count = item.Value.define.sellCount;
                }
            }
        }
    }


   
}
