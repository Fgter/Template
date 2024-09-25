public class TimeConverter
{
   public static float SecondToDay(float second)
    {
        return second / 86400;
    }

    public static float DayToSecond(float day)
    {
        return day * 86400;
    }

    public static float MinuteToDay(float second)
    {
        return second / 1140;
    }

    public static float DayToMinute(float day)
    {
        return day * 1140;
    }

    public static float HourToDay(float hour)
    {
        return hour / 24;
    }

    public static float DayToHour(float day)
    {
        return day * 24;
    }

    public static float SecondToHour(float second)
    {
        return second / 3600;
    }

    public static float MinuteToHour(float mintue)
    {
        return mintue / 60;
    }

    public static float HourToSecond(float hour)
    {
        return hour * 3600;
    }

    public static float MinuteToSecond(float minute)
    {
        return minute * 60;
    }


}
