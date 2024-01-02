using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public int itemId = 0;
    public Image Skin;
    public ItemType itemType;
    public Equipment equipment;
    public void SetItemStatus(ItemData _ItemData){
        itemId = _ItemData.itemId;
        Skin.sprite = _ItemData.itemSkin;
        itemType = _ItemData.itemType;
        equipment = _ItemData.equipment;
    }
}
