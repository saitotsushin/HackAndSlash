using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectArea : MonoBehaviour
{
    // public List<Enemy> EnemyList;
    [Tooltip("生成する範囲A")]
    public Transform RangeA;
    [SerializeField]
    [Tooltip("生成する範囲B")]
    public Transform RangeB;
    public List<GameObject> EnemyList;
    public GameObject Enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        Vector3 posA = RangeA.position;
        Vector3 posB = RangeB.position;
        // 全ての子要素にアクセス
        EnemyList = new List<GameObject>();
        foreach (Transform EnemyChild in Enemies.transform)
        {
            Enemy _Enemy = EnemyChild.gameObject.GetComponent<Enemy>();
            _Enemy.IsWait = true;
            // 子要素に対する処理
            Debug.Log("posA.x="+posA.x+"\n posB.x="+posB.x+"\n x=" + EnemyChild.position.x + "\n posA.y="+posA.y+"\n posB.y="+posB.y+"\n /y=" +  EnemyChild.position.y);
            if(posA.x < EnemyChild.position.x && EnemyChild.position.x < posB.x){
                if (posB.y < EnemyChild.position.y && EnemyChild.position.y < posA.y)
                {
                    _Enemy.IsWait = false;
                    EnemyList.Add(EnemyChild.gameObject);
                }
            }
        }
    }
}
