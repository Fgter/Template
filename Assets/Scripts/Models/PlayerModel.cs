using QFramework;

class PlayerModel : AbstractModel
{
    public float maxHp { get; set; }
    public BindableProperty<float> hp { get; set; } = new BindableProperty<float>();
    public int attack { get; set; }
    protected override void OnInit()
    {
        maxHp = 30;
        hp.SetValueWithoutEvent(maxHp);
        attack = 10;
    }
}
