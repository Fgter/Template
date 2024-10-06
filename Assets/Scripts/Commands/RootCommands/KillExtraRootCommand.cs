using QFramework;
class KillExtraRootCommand : AbstractCommand
{
    ExtraRootData data;
    public KillExtraRootCommand(ExtraRootData data)
    {
        this.data = data;
    }
    protected override void OnExecute()
    {
        this.GetModel<RootModel>().RemoveExtraRoot(data);
    }
}
