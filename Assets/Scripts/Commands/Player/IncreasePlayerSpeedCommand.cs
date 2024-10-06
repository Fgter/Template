using QFramework;
class IncreasePlayerSpeedCommand : AbstractCommand
{
    int value;
    public IncreasePlayerSpeedCommand(int value)
    {
        this.value = value;
    }
    protected override void OnExecute()
    {
        this.GetModel<PlayerModel>().speed += value;
    }
}
