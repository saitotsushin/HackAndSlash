using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIcon : MonoBehaviour
{
    public Player mPlayer;
    public GameObject spriteTargetIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTarget(GameObject _EnemyTarget){
        spriteTargetIcon.SetActive(true);
        spriteTargetIcon.transform.position = new Vector3(
            _EnemyTarget.transform.position.x,
            _EnemyTarget.transform.position.y,
            spriteTargetIcon.transform.position.z
        );
    }
    public void Hide(){
        spriteTargetIcon.SetActive(false);
    }
}
