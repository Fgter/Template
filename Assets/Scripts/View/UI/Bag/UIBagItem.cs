using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBagItem : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    TextMeshProUGUI count;
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    TextMeshProUGUI description;
    public void SetItem(Item item,int count)
    {
        this.icon.overrideSprite = ResLoader.LoadSprite(item.Icondefine.Icon);
        this.count.text = count.ToString();
        this.Name.text = item.Icondefine.Name;
        this.description.text = item.Icondefine.Description;
    }
}
