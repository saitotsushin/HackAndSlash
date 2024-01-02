using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject player;
    public Player mPlayer;
    private Vector3 beforePos;
    public bool IsAttacking = false;
    public Enemy _ParentEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack(Enemy _Enemy){
        Vector3 direction = (_Enemy.transform.position - transform.position).normalized;
        beforePos = transform.position;
        Vector3 afterPos = new Vector3(
            _Enemy.transform.position.x,
            _Enemy.transform.position.y,
            transform.position.z
        );
        _ParentEnemy = _Enemy;
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

        _ParentEnemy.Damage();

        this.gameObject.transform.position = beforePos;
    }
}
