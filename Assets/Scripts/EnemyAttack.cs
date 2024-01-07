using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    public Player mPlayer;
    private Vector3 beforePos;
    public Enemy EnemyBase;
    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        mPlayer = player.GetComponent<Player>();
        Transform myTransform = transform;
        Transform parentTransform = myTransform.parent;
        EnemyBase = parentTransform.gameObject.GetComponent<Enemy>();
    }
    private string playerTag = "Player";
    public void OnTriggerEnter2D(Collider2D collision) {
        if(EnemyBase.IsAttack){
            return;
        }
        if (collision.CompareTag(playerTag))
        {
            mPlayer.Damage(EnemyBase.ATK);
        }
    }
    public void Attack(){
        if(GManager.instance.GAMESTATUS == GAMESTATUS.GAMEOVER){
            return;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        beforePos = transform.position;
        Vector3 afterPos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );
        EnemyBase.IsAttack = true;
        iTween.MoveTo(gameObject,
            iTween.Hash(
                "position", afterPos,
                "time", 0.6f,
                "speed", 10f,
                "oncomplete","EndDamageAnimation"
            ));
    }
    public void EndDamageAnimation()
    {
        EnemyBase.IsAttack = false;
        // this.gameObject.transform.position = beforePos;
        this.gameObject.transform.position = EnemyBase.transform.position;
        EnemyBase.ResetActActiveTime();
    }
}
