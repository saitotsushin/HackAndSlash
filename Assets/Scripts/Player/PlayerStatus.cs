using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int StatusHp = 100;
    public int StatusAtk = 2;
    public int StatusDf = 2;
    public int StatusSpeed = 1;

    public int BaseStatusHp = 100;
    public int BaseStatusAtk = 2;
    public int BaseStatusDf = 2;
    public int BaseStatusSpeed = 1;
    
    public void UpdateEquipment(List<EquipmentSlot> _EquipmentList){
        StatusAtk = BaseStatusAtk;
        StatusDf = BaseStatusDf;
        StatusSpeed = BaseStatusSpeed;
        foreach(EquipmentSlot _Item in _EquipmentList){
            ItemData _ItemData = ItemManager.instance.GetItem(_Item.itemId);
            StatusAtk += _ItemData.ATK;
            StatusDf += _ItemData.DF;
            StatusSpeed += _ItemData.SPEED;
        }
    }
}
