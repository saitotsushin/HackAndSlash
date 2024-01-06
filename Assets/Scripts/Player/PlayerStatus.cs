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
    public float BaseStatusHunger = 100;
    public float StatusHunger = 100;
    public float ActActiveTime = 0f;
    public float BaseActActiveTime = 100f;
    public float ActActiveTimeSpeed = 2.0f;
    public Player Player;
    void Start(){   
    }

    public void SetUp(){
        float hpPar = (float)StatusHp / (float)BaseStatusHp;        
        Player.HpBar.UpdateHp(hpPar);        
    }
    
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
    public void UpdateActActiveTime(){
        ActActiveTime += 0.1f * ActActiveTimeSpeed;
        if(ActActiveTime >= BaseActActiveTime){
            ActActiveTime = BaseActActiveTime;
            Player.instance.CanAttack = true;
        }
        float par = ActActiveTime / BaseActActiveTime;
        Player.instance.mActActiveTime.UpdateBar(par);
    }
    public void ResetActActiveTime(){
        ActActiveTime = 0;
        Player.instance.mActActiveTime.UpdateBar(0.0f);
        Player.instance.CanAttack = false;
    }
}
