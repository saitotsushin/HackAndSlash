using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatArea : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("生成する範囲A")]
    public Transform RangeA;
    [SerializeField]
    [Tooltip("生成する範囲B")]
    public Transform RangeB;
    [Tooltip("プレイヤーとの距離")]
    public float Distance = 4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _moveDirection = Player.instance.move.Direction;
        Vector3 _moveDirectionNormalize = _moveDirection.normalized; 

        transform.position = new Vector3(
            _moveDirectionNormalize.x * Distance,
            _moveDirectionNormalize.y * Distance,
            0f
        );

    }
}
