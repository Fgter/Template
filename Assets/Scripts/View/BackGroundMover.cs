using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackGroundMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    float sensitivity = 1;
    [SerializeField]
    [Tooltip("x是较小值，y是较大值")]
    Vector2 horizontalArea;
    [Tooltip("x是较小值，y是较大值")]
    [SerializeField]
    Vector2 verticalArea;

    bool _darg;
    private void Update()
    {
        if (_darg && Input.touchCount > 0)
        {
            float x = Mathf.Clamp(Camera.main.transform.position.x - Input.GetTouch(0).deltaPosition.x * sensitivity * 0.01f, horizontalArea.x, horizontalArea.y);
            float y = Mathf.Clamp(Camera.main.transform.position.y - Input.GetTouch(0).deltaPosition.y * sensitivity * 0.01f, verticalArea.x, verticalArea.y);
            Camera.main.transform.position = new Vector3(x, y, -10);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _darg = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _darg = true;
    }
}
