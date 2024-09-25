using TMPro;
using UnityEngine;

public struct UITipData:IUIData
{
    public string message;
    public UITipData(string message)
    {
        this.message = message;
    }
}
public class UITip : UIWindowBase
{
    [SerializeField]
    TextMeshProUGUI tip;

    public override void OnShow(IUIData showData)
    {
        var data = (UITipData)showData;
        tip.text = data.message;
    }
}
