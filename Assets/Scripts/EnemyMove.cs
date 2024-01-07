using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D enemyRb;
    public GameObject player;
    public Player mPlayer;

    private string playerTag = "Player";
    private string enemyTag = "Enemy";
    public EnemyAttack mEnemyAttack;
    public Enemy EnemyBase;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag(playerTag);
        mPlayer = player.GetComponent<Player>();
        // Parent = transform.parent.gameObject.GetComponent<Enemy>();
        // Transform myTransform = transform;
        // Transform parentTransform = myTransform.parent;
        EnemyBase = transform.gameObject.GetComponent<Enemy>();        
    }


    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.GAMESTATUS == GAMESTATUS.GAMEOVER){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        if(EnemyBase.IsTouchPlayer){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        if(EnemyBase.IsAttack){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        if(EnemyBase.IsWait){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        if(mPlayer.IsActionMove == false){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRb.velocity = direction * speed;
        EnemyBase.UpdateActActiveTime();

    }


}
