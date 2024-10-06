using QFramework;

class PlayerHurtCommand : AbstractCommand
{
    PlayerModel model;
    public float value { get; set; }
    public PlayerHurtCommand(float value)
    {
        this.value = value;
    }
    protected override void OnExecute()
    {
        model = this.GetModel<PlayerModel>();
        model.hp.Value = model.hp.Value - value > 0 ? model.hp.Value - value : 0;
    }
}
