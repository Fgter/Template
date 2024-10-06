using QFramework;

class IncreaseGoldCommand : AbstractCommand
{
    int count;
    public IncreaseGoldCommand(int count)
    {
        this.count = count;
    }
    protected override void OnExecute()
    {
        this.GetModel<PlayerModel>().gold.Value += count;
    }
}
