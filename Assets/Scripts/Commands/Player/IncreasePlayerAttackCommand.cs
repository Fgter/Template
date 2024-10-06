using QFramework;
public class IncreasePlayerAttackCommand : AbstractCommand
{
    int value;
    public IncreasePlayerAttackCommand(int value)
    {
        this.value = value;
    }
    protected override void OnExecute()
    {
        this.GetModel<PlayerModel>().attack += value;
    }

   
}
