using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectFireAll : ItemEffect
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
        Debug.Log("ファイヤALLの効果詳細");
        if(ItemManager.instance.FieldTarget.Count > 0){
            for (int i = 0; i < ItemManager.instance.FieldTarget.Count; i++)
            {
                Transform enemyParentTransform = ItemManager.instance.FieldTarget[i].transform;
                Enemy mEnemy = enemyParentTransform.GetComponent<Enemy>();
                if(mEnemy){
                    Transform parentTransform = transform.parent;
                    MenuItem mMenuItem = parentTransform.GetComponent<MenuItem>();
                    int damagePoint = mMenuItem.point;
                    mEnemy.Damage(damagePoint);
                }
            }
        }
    }
}
