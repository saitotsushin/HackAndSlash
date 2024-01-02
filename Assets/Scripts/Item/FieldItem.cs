using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldItem : MonoBehaviour
{
    public int ItemId = 0;
    public Sprite ItemSprite;
    public void Awake()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ItemManager.instance.SetItem(ItemId);
            Destroy (this.gameObject);
        }
    }
    public void SetItemStatus(ItemData _ItemData){
        ItemSprite = _ItemData.itemSkin;
    }
}
