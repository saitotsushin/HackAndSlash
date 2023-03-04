using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D enemyRb;
    public GameObject player;
    private string playerTag = "Player";
    private string enemyTag = "Enemy";
    public bool IsCollided = false;
    public bool IsEnemyCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag(playerTag);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
        }
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("プレイヤーと接触している！");
        }
        IsCollided = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        IsCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        enemyRb.velocity = direction * speed;

        // transform.position = Vector2.MoveTowards(
        //     transform.position,
        //     player.transform.position,
        //     speed * Time.deltaTime
        // );

    }
}
