using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType {
    EQUIPMENT,
    ITEM,
    MAGIC
};
public enum Equipment {
    NONE,
    ARMOR,
    WEAPON,
    ACCESORIE
};
public enum UseTarget {
    PLAYER,
    ENEMY
};
public enum TargetRange{
    SINGLE,
    ALL
}
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int itemId;
    public string itemName;
    public ItemType itemType;
    public Equipment equipment;
    public UseTarget useTarget;
    public TargetRange targetRange;
    public Sprite itemSkin;
    public int ATK = 10;
    public int DF = 10;
    public int SPEED = 1;

    public int POINT = 0;
    public GameObject itemEffect;

}
