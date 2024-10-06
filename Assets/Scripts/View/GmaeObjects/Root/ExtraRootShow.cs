using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using QFramework;

public class ExtraRootShow : MonoBehaviour, IPointerClickHandler, IController
{
    public Vector3 startPos { get; set; }
    public float radius { get; set; }
    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        if (Input.GetMouseButton(1))
            Destroy(gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Vector3.Distance(startPos, transform.position) > radius)
            return;
        if (!this.SendCommand(new DecreaseGoldCommand(1)))
        {
            UIManager.instance.ShowTip("½ð±Ò²»¹»Äó");
            return;
        }
        this.SendCommand(new CreateExtraRootCommand(startPos, transform.position));
        Destroy(gameObject);
    }

    public IArchitecture GetArchitecture()
    {
        return PirateBomb.Interface;
    }
}
