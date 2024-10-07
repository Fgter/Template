using UnityEngine.UI;
using UnityEngine;
using QFramework;
using System;

class UIPlayerUp : UIWindowBase
{
    [SerializeField]
    Button btnAttackUp;
    [SerializeField]
    Button btnSpeedUp;
    [SerializeField]
    Button btnMaxHpUp;
    [SerializeField]
    Button btnClose;
    [SerializeField]
    Text gold;

    private void Start()
    {
        btnAttackUp.onClick.AddListener(AttackUp);
        btnSpeedUp.onClick.AddListener(SpeedUp);
        btnMaxHpUp.onClick.AddListener(MaxHpUp);
        btnClose.onClick.AddListener(() => Close());
        gold.text = this.GetModel<PlayerModel>().gold.Value.ToString();
        this.GetModel<PlayerModel>().gold.Register(OnGoldChanged).UnRegisterWhenGameObjectDestroyed(gameObject);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void MaxHpUp()
    {
        if (this.SendCommand(new DecreaseGoldCommand(1)))
            this.SendCommand(new IncreasePlayerMaxHpCommand(10));
        else
            UIManager.instance.ShowTip("金币不够捏");
    }

    private void SpeedUp()
    {
        if (this.SendCommand(new DecreaseGoldCommand(1)))
            this.SendCommand(new IncreasePlayerSpeedCommand(1));
        else
            UIManager.instance.ShowTip("金币不够捏");
    }

    private void AttackUp()
    {
        if (this.SendCommand(new DecreaseGoldCommand(1)))
            this.SendCommand(new IncreasePlayerAttackCommand(10));
        else
            UIManager.instance.ShowTip("金币不够捏");
    }

    private void OnGoldChanged(int obj)
    {
        gold.text = obj.ToString();
    }

    public void Close()
    {
        this.CloseNo();
        Time.timeScale = 1;
    }
}
