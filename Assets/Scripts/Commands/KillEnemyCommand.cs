using QFramework;
class KillEnemyCommand : AbstractCommand
{
    EnemyModel model;
    EnemyData data;
    public KillEnemyCommand(EnemyData data)
    {
        this.data = data;
    }
    protected override void OnExecute()
    {
        model = this.GetModel<EnemyModel>();
        model.RemoveEnemy(data);
    }
}
