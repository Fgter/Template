using QFramework;

class DecreaseGoldCommand : AbstractCommand<bool>
{
    int count;
    PlayerModel model;
    public DecreaseGoldCommand(int count)
    {
        this.count = count;
    }
    protected override bool OnExecute()
    {
        model = this.GetModel<PlayerModel>();
        if (model.gold.Value >= count)
        {
            model.gold.Value -= count;
            return true;
        }
        else
            return false;
    }
}
