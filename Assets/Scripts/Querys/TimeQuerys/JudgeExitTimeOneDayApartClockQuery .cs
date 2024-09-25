using QFramework;
using System;
using Models;

/// <summary>
/// //判断现在是不是上次退出的下一天的某一刻之后
/// </summary>
class JudgeExitTimeOneDayApartClockQuery : AbstractQuery<bool>
{
    TimeModel model;
    float clock;
    public JudgeExitTimeOneDayApartClockQuery(float clock)
    {
        this.clock = clock;
    }
    protected override bool OnDo()
    {
        model = this.GetModel<TimeModel>();
        DateTime currentTime = DateTime.Now;
        DateTime nextDayTime = model._lastExitTime.AddDays(1).Date.AddHours(clock);
        if (currentTime > nextDayTime)
            return true;
        return false;
    }
}
