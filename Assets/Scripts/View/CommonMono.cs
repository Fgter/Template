using QFramework;
using System;
using UnityEngine;
using System.Collections;

public class CommonMono : MonoSingleton<CommonMono>, IController
{
    static Action m_UpdateAction;
    static Action m_FixedUpdateAction;
    static Action m_QuitAction;

    public static void AddUpdateAction(Action fun) => m_UpdateAction += fun;
    public static void RemoveUpdateAction(Action fun) => m_UpdateAction -= fun;

    public static void AddFixedUpdateAction(Action fun) => m_FixedUpdateAction += fun;
    public static void RemoveFixedUpdateAction(Action fun) => m_FixedUpdateAction -= fun;

    public static void AddQuitAction(Action fun) => m_QuitAction += fun;
    public static void RemoveQuitAction(Action fun) => m_QuitAction -= fun;

    /// <summary>
    /// 在非mono脚本中开启协程，所以协程的关闭条件需要额外控制
    /// </summary>
    /// <param name="ienum"></param>
    /// <returns></returns>
    public new Coroutine StartCoroutine(IEnumerator ienum)
    {
        return StartCoroutine(ienum);
    }

    public new void StopCoroutine(Coroutine cor)
    {
        StopCoroutine(cor);
    }

    public new void StopAllCoroutines()
    {
        StopAllCoroutines();
    }

    protected override void OnAwake()
    {
        this.GetArchitecture();
    }
    private void Update()
    {
        m_UpdateAction?.Invoke();
    }

    private void FixedUpdate()
    {
        m_FixedUpdateAction?.Invoke();
    }

    private void OnApplicationQuit()
    {
        m_QuitAction?.Invoke();
    }

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
}
