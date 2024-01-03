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


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag(playerTag);
        mPlayer = player.GetComponent<Player>();
    }


    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.GAMESTATUS == GAMESTATUS.GAMEOVER){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        // if(mEnemyAttack.IsAttacking){
        //     return;
        // }
        if(mPlayer.IsActionMove == false){
            enemyRb.velocity = Vector2.zero;
            return;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRb.velocity = direction * speed;

    }

}
