using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropAreaField : MonoBehaviour
{
    public Image imageComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetRaycastTarget(bool _isTarget){
        imageComponent.raycastTarget = _isTarget;
        // 現在の色を取得
        Color currentColor = imageComponent.color;
        float newAlpha = 0f; // 例: アルファ値を半透明に設定

        if(_isTarget){
            // 新しいアルファ値を指定
            newAlpha = 0.75f; // 例: アルファ値を半透明に設定
        }else{
            newAlpha = 0f; // 例: アルファ値を半透明に設定
        }
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);      
        // Imageの色を変更
        imageComponent.color = newColor;            
    }
}
