using QFramework;
using System;
using Models;

/// <summary>
/// //判断现在是不是上次退出的下一天的某一刻之后
/// </summary>
class JudgeTimeOneDayApartClock : AbstractQuery<bool>
{
    TimeModel model;
    float clock;
    DateTime lastTime;
    public JudgeTimeOneDayApartClock(float clock, DateTime lastTime)
    {
        this.clock = clock;
        this.lastTime = lastTime;
    }
    protected override bool OnDo()
    {
        model = this.GetModel<TimeModel>();
        DateTime currentTime = DateTime.Now;
        DateTime nextDayTime = lastTime.AddDays(1).Date.AddHours(clock);
        if (currentTime > nextDayTime)
            return true;
        return false;
    }


}
