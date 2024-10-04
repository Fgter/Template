using QFramework;

class PlayerModel : AbstractModel
{
    public BindableProperty<int> hp { get; set; } = new BindableProperty<int>();
    protected override void OnInit()
    {
        hp.SetValueWithoutEvent(3);
    }
}
