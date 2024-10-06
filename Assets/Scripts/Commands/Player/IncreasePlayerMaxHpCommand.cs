using QFramework;
class IncreasePlayerMaxHpCommand : AbstractCommand
{
    PlayerModel model;
    float value;
    public IncreasePlayerMaxHpCommand(float value)
    {
        this.value = value;
    }
    protected override void OnExecute()
    {
        model = this.GetModel<PlayerModel>();
        model.maxHp.Value += value;
        model.hp.Value += value;
    }
}
