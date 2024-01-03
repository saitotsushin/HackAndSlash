using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Camera _mainCamera;
    public FloatingJoystick joystick;//FixedJoystickを取得
    
    private Rigidbody2D rb;
    public float MoveSpeed;
    Vector3 joystickMoveVector;  //ジョイスティックの傾き度を取得

    public Vector2 Direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        //攻撃中は移動できない
        if(Player.instance.IsActionAttack){
            rb.velocity = Vector2.zero;
            return;
        }

        joystickMoveVector = Vector3.right * joystick.Horizontal * MoveSpeed + Vector3.up * joystick.Vertical * MoveSpeed;
        if (joystickMoveVector != Vector3.zero)  //ジョイスティックを動かすと動く。
        {
            Vector2 v = new Vector2(joystickMoveVector.x,joystickMoveVector.y);
            rb.velocity = v;
            Direction = v;
            Player.instance.IsActionMove = true;
            Player.instance.Hunger();
        }else{
            Player.instance.IsActionMove = false;
            rb.velocity = Vector2.zero;
        }
    }
}
