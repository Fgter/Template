using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageTip : UIWindowBase
{
    [SerializeField]
    Image image;
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    Sprite error;//固定显示
    [SerializeField]
    Sprite warning;//固定显示
    [SerializeField]
    Sprite finish;//固定显示
    [SerializeField]
    Sprite common;//非固定图标
    [SerializeField]
    Button btn;
    Action action;
    MessageType type = MessageType.Common;//默认消息类型
    public override void OnShow(IUIData showData)
    {
        if(showData != null)
        {
            if (showData is MessageTipData)
            {
                MessageTipData messageTipData = (MessageTipData)showData;
                SetTip(messageTipData.message);
            }
            else
            {
                Debug.LogError("[UIMessageTip] 传入的类型不正确");
            }
        }
        action = () => { UIManager.instance.Close(typeof(UIMessageTip)); };
        btn.onClick.AddListener(() =>
        {
            action?.Invoke();
        });
    }
    public UIMessageTip SetTip(string tip)
    {
        text.text = tip;
        SetType(type);
        return this;
    }
    public UIMessageTip SetTip(string tip, Action action = null)
    {
        text.text = tip;
        SetType(type);
        if (action != null)
        {
            AddFunction(action);
        }
        return this;
    }
    public UIMessageTip SetTip(string tip, Sprite sprite = null, Action action = null)
    {
        text.text = tip;
        SetType(type);
        if (sprite != null)
        {
            SetIcon(sprite);
        }
        if (action != null)
        {
            AddFunction(action);
        }
        return this;
    }
    /// <summary>
    /// 设定显示的消息
    /// </summary>
    /// <param name="tip"></param>
    /// <returns></returns>
    public UIMessageTip SetTip(string tip,MessageType messageType=default,Action action = null)
    {
        text.text = tip;
        if(messageType != default)
        {
            SetType(messageType);
        }
        if (action != null)
        {
            AddFunction(action);
        }
        return this;
    }
    /// <summary>
    /// 设定显示的信息类型
    /// </summary>
    /// <param name="messageType"></param>
    /// <returns></returns>
    public UIMessageTip SetType(MessageType messageType)
    {
        type = messageType;
        switch (messageType)
        {
            case MessageType.Error:
                image.sprite = error;
                break;
            case MessageType.Warning:
                image.sprite = warning;
                break;
            case MessageType.Common:
                image.sprite = common;
                break;
            case MessageType.Finish:
                image.sprite = finish;
                break;
            default:
                break;
        }
        return this;
    }
    /// <summary>
    /// 设定普通类型下显示的图片
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    public UIMessageTip SetIcon(Sprite sprite)
    {
        if (type == MessageType.Common)
        {
            common = sprite;
            image.sprite = common;
        }
        else
        {
            Debug.LogError($"[UIMessageTip] 非MessageType.Common类型消息不可自定义图片样式请先修改消息类型再调用SetIcon()");
            //建议修改方式:SetType(MessageType.Common).SetIcon(xxxx);
        }
        return this;
    }
    /// <summary>
    /// 给确定按钮添加方法
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public UIMessageTip AddFunction(Action action)
    {
        this.action += action;
        return this;
    }
}
public enum MessageType
{
    Error,
    Warning,
    Common,
    Finish
}
