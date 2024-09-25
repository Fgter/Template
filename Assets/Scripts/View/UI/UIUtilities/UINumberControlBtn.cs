using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINumberControlBtn : MonoBehaviour
{
    Action OnBtnConfirm;
    public int maxNumber { get; private set; }
    public int currentNumber { get; private set; }

    [SerializeField]
    Button btnIncrease;
    [SerializeField]
    Button btnDecrease;
    [SerializeField]
    Button btnMax;
    [SerializeField]
    Button btnMin;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Button btnConfirm;
    private void Start()
    {
        btnIncrease.onClick.AddListener(IncreaseNumber);
        btnDecrease.onClick.AddListener(DecreaseNumber);
        btnMax.onClick.AddListener(MaxNumber);
        btnMin.onClick.AddListener(MinNumber);
        btnConfirm.onClick.AddListener(() => OnBtnConfirm?.Invoke());
        btnConfirm.onClick.AddListener(ResetNumber);
        ResetNumber();
    }

    void ResetNumber()
    {
        currentNumber = Math.Min(maxNumber, 1);
        Refresh();
    }

    public void SetMaxNumber(int number)
    {
        maxNumber = Math.Max(0, number);
        currentNumber = Math.Min(maxNumber, currentNumber);
        Refresh();
    }

    public void RegisterBtnConfirmFunc(Action func) => OnBtnConfirm += func;
    public void UnRegisterBtnConfirmFunc(Action func) => OnBtnConfirm -= func;
    void MinNumber()
    {
        currentNumber = 0;
        Refresh();
    }

    void MaxNumber()
    {
        currentNumber = maxNumber;
        Refresh();
    }

    void DecreaseNumber()
    {
        currentNumber = Math.Max(currentNumber - 1, 0);
        Refresh();
    }

    void IncreaseNumber()
    {
        currentNumber = Math.Min(currentNumber + 1, maxNumber);
        Refresh();
    }

    void Refresh()
    {
        text.text = currentNumber.ToString();
        int status = 0;
        if (currentNumber >= maxNumber)
            status = 1;
        btnIncrease.interactable = status != 1;
        btnMax.interactable = status != 1;

        if (currentNumber <= 0)
            status = -1;
        btnDecrease.interactable = status != -1;
        btnMin.interactable = status != -1;

        if (btnConfirm != null)
            btnConfirm.interactable = currentNumber != 0;
    }
}
