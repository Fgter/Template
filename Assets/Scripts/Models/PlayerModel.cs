using QFramework;
using System;

class PlayerModel : AbstractModel
{
    public BindableProperty<float> maxHp { get; set; } = new BindableProperty<float>();
    public BindableProperty<float> hp { get; set; } = new BindableProperty<float>();
    public int attack { get; set; }
    public float speed { get; set; }

    public BindableProperty<int> gold { get; set; } = new BindableProperty<int>();
    protected override void OnInit()
    {
        maxHp.Value = 30;
        hp.SetValueWithoutEvent(maxHp.Value);
        attack = 10;
        speed = 5;
        hp.Register(OnHpChanged);
    }

    private void OnHpChanged(float currentHp)
    {
        if (currentHp > maxHp.Value)
            hp.Value = maxHp.Value;
    }
}
