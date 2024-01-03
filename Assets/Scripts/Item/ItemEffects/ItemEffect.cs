using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Effect(){
        Debug.Log("アイテムEFFECTS");
        EffectDetail();
    }
    public virtual void EffectDetail(){
        Debug.Log("ベースの効果詳細");
    }
}
