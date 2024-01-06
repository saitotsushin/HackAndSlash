using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public int itemId = 0;
    public Image Skin;
    public ItemType itemType;
    public Equipment equipment;
    public UseTarget useTarget;
    public TargetRange targetRange;

    public int point = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetItemStatus(ItemData _ItemData){
        itemId = _ItemData.itemId;
        Skin.sprite = _ItemData.itemSkin;
        itemType = _ItemData.itemType;
        equipment = _ItemData.equipment;
        point = _ItemData.POINT;
        useTarget = _ItemData.useTarget;
        targetRange = _ItemData.targetRange;
    }
    public void Fire(){
        Debug.Log("アイテムの発火");
        foreach (Transform child in transform)
        {
            // 取得した子要素に対して処理を行う
            ItemEffect itemEffect = child.GetComponent<ItemEffect>();
            itemEffect.Effect();
        }        

    }
}
