using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectPotion : ItemEffect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void EffectDetail(){
        Debug.Log("ポーションの効果詳細");
        Transform parentTransform = transform.parent;
        MenuItem mMenuItem = parentTransform.GetComponent<MenuItem>();
        Player.instance.Feel(mMenuItem.point);
    }
}
