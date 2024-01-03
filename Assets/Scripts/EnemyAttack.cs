using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    public Player mPlayer;
    public bool IsAttacking = false;
    private Vector3 beforePos;
    void Start()
    {
        player = GameObject.FindWithTag(playerTag);
        mPlayer = player.GetComponent<Player>();
    }
    private string playerTag = "Player";
    public void OnTriggerEnter2D(Collider2D collision) {
        if(IsAttacking){
            return;
        }
        if (collision.CompareTag(playerTag))
        {
            mPlayer.Damage();
        }
    }
    public void Attack(){
        Vector3 direction = (player.transform.position - transform.position).normalized;
        beforePos = transform.position;
        Vector3 afterPos = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            transform.position.z
        );
        IsAttacking = true;
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
        IsAttacking = false;
        this.gameObject.transform.position = beforePos;
    }
}
