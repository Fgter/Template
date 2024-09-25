using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectableImageGroup
{
    public class UISelectableImage : MonoBehaviour, IPointerClickHandler
    {
        public bool selected { get; set; }
        public UISelectableImageGroup owner { get; set; }
        public CanvasGroup selectBox { get => _selectBox; }

        [SerializeField]
        CanvasGroup _selectBox;
        public void OnPointerClick(PointerEventData eventData)
        {
            owner.Select(this);
        }
    }
    public UISelectableImage currentSelect { get; private set; }
    Action<Transform> OnSelect;
    Action<Transform> OnDeSelect;
    List<UISelectableImage> elements = new List<UISelectableImage>();

    public void AddElement(UISelectableImage element)
    {
        if (!elements.Contains(element))
            elements.Add(element);
        element.owner = this;
    }

    public void RemoveElement(UISelectableImage element)
    {
        if (elements.Contains(element))
        {
            GameObject.Destroy(element.gameObject);
            elements.Remove(element);
        }
    }

    public void ClearElements()
    {
        foreach (var e in elements)
        {
            GameObject.Destroy(e.gameObject);
        }
        elements.Clear();
    }

    public void RegisterSelectAction(Action<Transform> func) => OnSelect += func;
    public void UnRegisterSelectAction(Action<Transform> func) => OnSelect -= func;
    public void RegisterDeSelectAction(Action<Transform> func) => OnDeSelect += func;
    public void UnRegisterdESelectAction(Action<Transform> func) => OnDeSelect -= func;

    public void Select(UISelectableImage element)
    {
        foreach (var e in elements)
            DeSelect(e);
        currentSelect = element;
        element.selected = true;
        element.selectBox.alpha = 1;
        OnSelect?.Invoke(element.transform);
    }

    void DeSelect(UISelectableImage element)
    {
        element.selected = false;
        element.selectBox.alpha = 0;
        OnDeSelect?.Invoke(element.transform);
    }
}
