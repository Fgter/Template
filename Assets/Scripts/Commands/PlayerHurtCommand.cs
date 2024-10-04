using QFramework;

class PlayerHurtCommand : AbstractCommand
{
    PlayerModel model;
    protected override void OnExecute()
    {
        model = this.GetModel<PlayerModel>();
        model.hp.Value = model.hp.Value - 1 > 0 ? model.hp.Value - 1 : 0;
    }
}
