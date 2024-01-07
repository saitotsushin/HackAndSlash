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
    private int BaseHP = 10;
    public int ATK = 10;
    public float ActActiveTime = 0f;
    private float BaseActActiveTime = 100f;
    public float ActActiveTimeSpeed = 2.0f;

    public SpriteRenderer SpriteImage;

    public int DropItemId = 0;

    public bool IsActive = true;
    public bool IsWait = true;
    public bool IsTouchPlayer = false;
    public ActActiveTime mActActiveTime;
    public bool CanAttack = false;
    public HpBar mHpBar;
    public GameObject CursorArrow;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        mPlayer = player.GetComponent<Player>();
        BaseHP = HP;
    }
    void Update(){
        if(IsWait){
            return;
        }
        if(IsTouchPlayer && CanAttack){
            Attack();
            CanAttack = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
        }
        if (collision.gameObject.CompareTag(playerTag))
        {
            IsTouchPlayer = true;
            // if(!CanAttack){
            //     return;
            // }
            // Attack();
            // IsAttack = true;            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            // CanAttack = false;
            IsTouchPlayer = false;
        }
        // IsAttack = false;
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
        float hpPar = (float)HP / (float)BaseHP;
        mHpBar.UpdateHp(hpPar);
        if(HP <= 0){
            if(IsActive){
                ItemManager.instance.CreateFieldItem(DropItemId,this.gameObject.transform.position);
            }
            IsActive = false;
            Destroy (this.gameObject);
        }
    }
    public void UpdateActActiveTime(){
        ActActiveTime += 0.1f * ActActiveTimeSpeed;
        if(ActActiveTime >= BaseActActiveTime){
            ActActiveTime = BaseActActiveTime;
            CanAttack = true;
        }
        float par = ActActiveTime / BaseActActiveTime;
        mActActiveTime.UpdateBar(par);
    }
    public void ResetActActiveTime(){
        ActActiveTime = 0;
        mActActiveTime.UpdateBar(0.0f);
        CanAttack = false;
    }
}
