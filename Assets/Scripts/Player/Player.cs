using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player instance;

    
    public bool IsActionMove = false;
    public bool IsActionAttack = false;
    public bool IsDamage = false;
    private GameObject AttackEnemy;
    public Move mMove;
    public PlayerStatus mPlayerStatus;
    public PlayerAttack mPlayerAttack;
    public PlayerImage mPlayerImage;
    public PlayerHunger mPlayerHunger;
    public AttackTargetCircle attackTargetCircle;
    public bool IsAlive = true;

    private float time;
    public float ActionTime = 1.0f;
    void Awake ()
    {
        if (instance == null) {
        
            instance = this; 
        }
        else {
            Destroy (gameObject);
        }
        mMove = GetComponent<Move>();
        mPlayerHunger = GetComponent<PlayerHunger>();
    }
    
    void Start()
    {
    }
    void Update()
    {
        if(!IsAlive){
            return;
        }
        time = time + Time.deltaTime;
        if (time > ActionTime)
        {
            CheckAutoAttack();
            time = 0f;
        }

    }
    public void SetUp(){
        mPlayerStatus.SetUp();
        mPlayerHunger.SetUp();
    }
    public void CheckAutoAttack(){
        Enemy _Enemy = attackTargetCircle.GetNearEnemy(this.gameObject);
        if(_Enemy){       
            AttackEnemy = _Enemy.gameObject;
            mPlayerAttack.Attack(_Enemy);
        }
    }
    public void Damage(int _SetPoint){
        mPlayerImage.DamageEffect();
        mPlayerStatus.StatusHp -= _SetPoint;
        SetHpBar();
        if(mPlayerStatus.StatusHp <= 0){
            IsAlive = false;
            GManager.instance.GameOver();
        }
    }
    public void Feel(int _SetPoint){
        mPlayerImage.FeelEffect();
        mPlayerStatus.StatusHp += _SetPoint;
        if (mPlayerStatus.StatusHp >= mPlayerStatus.BaseStatusHp)
        {
            mPlayerStatus.StatusHp = mPlayerStatus.BaseStatusHp;
        }
        SetHpBar();

    }
    public void SetHpBar(){
        float hpPar = (float)mPlayerStatus.StatusHp / (float)mPlayerStatus.BaseStatusHp;
        HpBar.instance.UpdateHp(hpPar);        
    }
    public void Hunger(){
        mPlayerHunger.Hunger();
    }
    public void HungerSetPoint(int _point){
        mPlayerHunger.HungerSetPoint(_point);
    }

}
