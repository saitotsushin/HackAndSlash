using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectFire : ItemEffect
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
        Debug.Log("ファイヤの効果詳細");
        Transform enemyParentTransform = ItemManager.instance.FieldTarget.transform.parent;
        Enemy mEnemy = enemyParentTransform.GetComponent<Enemy>();
        if(mEnemy){
            Transform parentTransform = transform.parent;
            MenuItem mMenuItem = parentTransform.GetComponent<MenuItem>();
            int damagePoint = mMenuItem.point;
            mEnemy.Damage(damagePoint);
        }
    }
}
