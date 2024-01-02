using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType {
    EQUIPMENT,
    ITEM,
    MAGIC
};
public enum Equipment {
    ARMOR,
    WEAPON,
    ACCESORIE
};
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int itemId;
    public ItemType itemType;
    public Equipment equipment;
    public Sprite itemSkin;
    public int ATK = 10;
    public int DF = 10;
    public int SPEED = 1;

    public int FEEL = 0;
    public int HEALTH = 0;

}
