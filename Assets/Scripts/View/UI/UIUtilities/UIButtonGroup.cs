using UnityEngine;
using UnityEngine.UI;

public class UIButtonGroup : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;
    [SerializeField]
    Button InitialSelectedBtn;

    [SerializeField]
    Color activeColor;
    [SerializeField]
    Color disactiveColor;
    private void Awake()
    {
        foreach (var btn in buttons)
        {
            btn.onClick.AddListener(() => ActiveBtn(btn));
        }
    }

    void ActiveBtn(Button btn)
    {
        foreach (var b in buttons)
        {
            b.image.color = disactiveColor;
        }
        btn.image.color = activeColor;
    }

    public void ActiveInitialSelectedBtn()
    {
        if(InitialSelectedBtn!=null)
        {
            ActiveBtn(InitialSelectedBtn);
        }
    }
}
