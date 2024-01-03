using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //追加し忘れ注意

public class HpBar : MonoBehaviour
{
    static public HpBar instance;
    // Start is called before the first frame update
    private float maxGuage = 100f;
    private float nowGuage = 0;
    public Image HpDispBar = null;
    private RectTransform cachedRectTransform;
    private float hp_x;
    void Awake ()
    {
        if (instance == null) {
        
            instance = this; 
        }
        else {
            Destroy (gameObject);
        }
        cachedRectTransform = HpDispBar.GetComponent<RectTransform>();
        hp_x = cachedRectTransform.sizeDelta.x;
    }
    void Start()
    {
        
        
    }
    public void UpdateHp(float par){
        Debug.Log("par=" + par);
        // float setHp = (float)hp;
        
        float SetPar = hp_x * par;
        cachedRectTransform.sizeDelta = new Vector2(SetPar, cachedRectTransform.sizeDelta.y);
    }
}
