using System;
using QFramework;
using Models;

/// <summary>
/// 获得下线的时间长度(day)
/// </summary>
class GetOfflinePeriodQuery : AbstractQuery<float>
{
    TimeModel model;
    protected override float OnDo()
    {
        model = this.GetModel<TimeModel>();
        if (model._lastExitTime != null)
            return TimeConverter.SecondToDay((float)(DateTime.Now - model._lastExitTime).TotalSeconds);
        else
            return 0;
    }
}
