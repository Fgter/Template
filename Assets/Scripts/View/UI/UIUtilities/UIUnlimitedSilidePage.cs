using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnlimitedSilidePage : MonoBehaviour
{
    public RectTransform[] PageTransforms { get => pageRects; }
    public Page currentPage { get; private set; }
    public class Page
    {
        public Page lastPage { get; set; }
        public RectTransform rectTransform { get; set; }
        public int index { get; set; }
        public Page nextPage { get; set; }
        public Page(RectTransform rectTransform,int index)
        {
            this.rectTransform = rectTransform;
            this.index = index;
        }
    }
    Action<RectTransform> OnLastPage;
    Action<RectTransform> OnNextPage;
    //Action<RectTransform> OnShowPage;
    /// <summary>
    /// 按从上到下的顺序放入3张page
    /// </summary>
    [SerializeField]
    RectTransform[] pageRects = new RectTransform[3];   
    [SerializeField]
    float interval = 1200;
    [SerializeField]
    Button btnLastPage;
    [SerializeField]
    Button btnNextPage;

    Page[] pages = new Page[3];
    bool animating;
    Dictionary<int, List<RectTransform>> pageElements = new Dictionary<int, List<RectTransform>>();
    private void Awake()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pageElements[i] = GetChilds(pageRects[i]);
        }
        pages = LinkPage(pageRects);
        currentPage = pages[1];
        btnLastPage.onClick.AddListener(LastPage);
        btnNextPage.onClick.AddListener(NextPage);
    }

    //private void OnEnable()
    //{
    //    OnShowPage?.Invoke(currentPage.rectTransform);
    //}

    public void RegisterNextPageFunc(Action<RectTransform> func) => OnNextPage += func;
    public void UnRegisterNextPageFunc(Action<RectTransform> func) => OnNextPage -= func;
    public void RegisterLastPageFunc(Action<RectTransform> func) => OnLastPage += func;
    public void UnRegisterLastPageFunc(Action<RectTransform> func) => OnLastPage -= func;
    //public void RegisterShowPageFunc(Action<RectTransform> func) => OnShowPage += func;
    //public void UnRegisterShowPageFunc(Action<RectTransform> func) => OnShowPage -= func;

    private void NextPage()
    {
        if (animating)
            return;
        #region DoAnim
        animating = true;
        Vector3 currentPos = currentPage.rectTransform.anchoredPosition;
        DOVirtual.Float(currentPos.y, interval, 0.5f, v =>
        {
            currentPage.rectTransform.anchoredPosition = new Vector3(currentPos.x, v, currentPos.z);
        });
        Vector3 lastPos = currentPage.lastPage.rectTransform.anchoredPosition;
        currentPage.lastPage.rectTransform.anchoredPosition = new Vector3(lastPos.x, -interval, lastPos.z);
        Vector3 nextPos = currentPage.nextPage.rectTransform.anchoredPosition;
        DOVirtual.Float(nextPos.y, 0, 0.5f, v =>
        {
            currentPage.nextPage.rectTransform.anchoredPosition = new Vector3(nextPos.x, v, nextPos.z);
        }).onComplete += () =>
        {
            currentPage = currentPage.nextPage;
            animating = false;
        };
        #endregion 
        OnNextPage?.Invoke(pageRects[currentPage.nextPage.index]);
    }

    private void LastPage()
    {
        if (animating)
            return;
        #region DoAnim
        animating = true;
        Vector3 currentPos = currentPage.rectTransform.anchoredPosition;
        DOVirtual.Float(currentPos.y, -interval, 0.5f, v =>
        {
            currentPage.rectTransform.anchoredPosition = new Vector3(currentPos.x, v, currentPos.z);
        });
        Vector3 nextPos = currentPage.lastPage.rectTransform.anchoredPosition;
        currentPage.nextPage.rectTransform.anchoredPosition = new Vector3(nextPos.x, interval, nextPos.z);
        Vector3 lastPos = currentPage.lastPage.rectTransform.anchoredPosition;
        DOVirtual.Float(lastPos.y, 0, 0.5f, v =>
        {
            currentPage.lastPage.rectTransform.anchoredPosition = new Vector3(lastPos.x, v, lastPos.z);
        }).onComplete += () =>
         {
             currentPage = currentPage.lastPage;
             animating = false;
         };
        #endregion
        OnLastPage?.Invoke(pageRects[currentPage.lastPage.index]);
    }

    List<RectTransform> GetChilds(RectTransform rt)
    {
        RectTransform[] rts = rt.GetComponentsInChildren<RectTransform>();
        List<RectTransform> result = new List<RectTransform>(rts.Length);
        foreach (var r in rts)
        {
            if (r != rt)
                result.Add(r);
        }
        return result;
    }

    Page[] LinkPage(RectTransform[] pages)
    {
        Page[] result = new Page[3];
        Page p1 = new Page(pages[0],0);
        Page p2 = new Page(pages[1],1);
        Page p3 = new Page(pages[2],2);
        p1.nextPage = p2; p1.lastPage = p3;
        p2.nextPage = p3; p2.lastPage = p1;
        p3.nextPage = p1; p3.lastPage = p2;
        result[0] = p1; result[1] = p2; result[2] = p3;
        return result;
    }
}

