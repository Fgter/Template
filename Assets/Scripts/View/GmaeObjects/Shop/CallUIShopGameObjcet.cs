using UnityEngine;
using UnityEngine.EventSystems;

public class CallUIShopGameObjcet : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    int shopId;

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.Show<UIShop>(new UIShopData(shopId));
    }
}
