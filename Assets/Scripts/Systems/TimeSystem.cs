using QFramework;
using SaveData;
using System;
using UnityEngine;

public class TimeSystem : AbstractSystem
{
    static Action _SecondAction;
    static Action<DateTime> _ClockAction;
    static float _secondTimer;
    protected override void OnInit()
    {
        CommonMono.AddFixedUpdateAction(() =>
        {
            _secondTimer += Time.fixedDeltaTime;
            if (_secondTimer >= 1)
            {
                _SecondAction?.Invoke();
                _secondTimer = 0;
            }
        });

        _SecondAction += () =>
          {
              if (JudgeExactHourOrHalfHour(DateTime.Now))
                  _ClockAction?.Invoke(DateTime.Now);
          };

       
    }

    public static void RegisterSecondUpdateAction(Action fun) => _SecondAction += fun;
    public static void UnRegisterSecondUpdateAction(Action fun) => _SecondAction -= fun;
    public static void RegisterClockUpdateAction(Action<DateTime> fun) => _ClockAction += fun;
    public static void UnRegisterClockUpdateAction(Action<DateTime> fun) => _ClockAction -= fun;


    bool JudgeExactHourOrHalfHour(DateTime time) //判断是不是半点或整点
    {
        return (time.Minute == 0 || time.Minute == 30) && time.Second == 0;
    }
}
