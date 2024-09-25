using QFramework;
using Models;
public class GetGoldQuery : AbstractQuery<int>
{
    protected override int OnDo()
    {
       return  this.GetModel<PlayerModel>().Gold.Value;
    }
}
