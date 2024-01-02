using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImage : MonoBehaviour
{
    public SpriteRenderer SpriteImage;

    // Update is called once per frame
    public void DamageEffect()
    {
        StartCoroutine(
            Utils.DelayMethod(2.0f, () =>
                {
                    // 通常状態に戻す
                    Player.instance.IsDamage = false;
                    SpriteImage.color = new Color(1f, 1f, 1f, 1f);
                }
            )
        );  
    }
}
