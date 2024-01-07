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
    // public float Speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if(GManager.instance.GAMESTATUS == GAMESTATUS.GAMEOVER){
            rb.velocity = Vector2.zero;
            return;
        }
        //攻撃中は移動できない
        // if(Player.instance.IsActionAttack){
        //     rb.velocity = Vector2.zero;
        //     return;
        // }

        joystickMoveVector = Vector3.right * joystick.Horizontal * MoveSpeed + Vector3.up * joystick.Vertical * MoveSpeed;
        if(joystick.IsClicked){
            Player.instance.mPlayerStatus.UpdateActActiveTime();            
            Player.instance.IsActionMove = true;
        }else{
            Player.instance.IsActionMove = false;
        }
        if (joystickMoveVector != Vector3.zero)  //ジョイスティックを動かすと動く。
        {
            Vector2 v = new Vector2(joystickMoveVector.x,joystickMoveVector.y);
            rb.velocity = v;
            Direction = v;
            Player.instance.Hunger();
        }else{
            rb.velocity = Vector2.zero;
        }
    }
}
