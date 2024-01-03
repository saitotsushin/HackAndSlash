using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool IsActionArea = false;
    private string playerTag = "Player";
    private string enemyTag = "Enemy";
    public GameObject player;
    public Player mPlayer;

    public bool IsAttack = false;
    public EnemyAttack mEnemyAttack;

    public int HP = 10;
    public int ATK = 10;

    public SpriteRenderer SpriteImage;

    public int DropItemId = 0;

    public bool IsActive = true;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        mPlayer = player.GetComponent<Player>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
        }
        if (collision.gameObject.CompareTag(playerTag))
        {
            Attack();
            IsAttack = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        IsAttack = false;
    }
    public void Attack(){
        float _DelayTime = 1.0f;
        mEnemyAttack.Attack();
        StartCoroutine(
            Utils.DelayMethod(_DelayTime, () =>
                {
                    // mEnemyAttack.Attack();
                }
            )
        );
    }
    public void Damage(int _point = 2)
    {
        HP -= _point;
        SpriteImage.color = new Color(1f, 0.5f, 0.5f, 1f);
        StartCoroutine(
            Utils.DelayMethod(0.2f, () =>
                {
                    if(this.gameObject){
                        SpriteImage.color = new Color(0.6f, 0.6f, 0.6f, 1f);
                    }
                }
            )
        );
        if(HP <= 0){
            if(IsActive){
                ItemManager.instance.CreateFieldItem(DropItemId,this.gameObject);
            }
            IsActive = false;
            Destroy (this.gameObject);
        }
    }
}
