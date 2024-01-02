using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetCircle : MonoBehaviour
{
    private string enemyTag = "Enemy";
    public List<Enemy> EnemyList;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Enemy _Enemy = collision.gameObject.GetComponent<Enemy>();
            _Enemy.IsActionArea = true;
            EnemyList.Add(_Enemy);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Enemy _Enemy = collision.gameObject.GetComponent<Enemy>();
            _Enemy.IsActionArea = false;
        }
        EnemyListUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public Enemy GetNearEnemy(GameObject _Player){
        Enemy GetEnemy = null;
        float closeDist = 1000;
        foreach(Enemy _Enemy in EnemyList){
            // このオブジェクト（砲弾）と敵までの距離を計測
            float tDist = Vector3.Distance(_Player.transform.position, _Enemy.gameObject.transform.position);

            // もしも「初期値」よりも「計測した敵までの距離」の方が近いならば、
            if(closeDist > tDist)
            {
                // 「closeDist」を「tDist（その敵までの距離）」に置き換える。
                // これを繰り返すことで、一番近い敵を見つけ出すことができる。
                closeDist = tDist;
 
                // 一番近い敵の情報をcloseEnemyという変数に格納する（★）
                GetEnemy = _Enemy;
            }
        }
        return GetEnemy;
    }
    public void EnemyListUpdate(){
        List<Enemy> newEnemyList = new List<Enemy>();
        foreach(Enemy _Enemy in EnemyList){
            if(_Enemy.IsActionArea){
                newEnemyList.Add(_Enemy);
            }
        }
        EnemyList = newEnemyList;
    }
}
