using Models;
using QFramework;
class IncreaseGoldCommand : AbstractCommand<int>
{
    int count;
    public IncreaseGoldCommand(int count)
    {
        this.count = count;
    }
    protected override int OnExecute()
    {
        this.GetModel<PlayerModel>().Gold.Value += count;
        return count;
    }
}
