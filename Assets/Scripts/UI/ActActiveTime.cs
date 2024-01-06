using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActActiveTime : MonoBehaviour
{
    public Image ActiveDispBar = null;
    private RectTransform cachedRectTransform;
    public float active_x;
    void Awake ()
    {
        // if (instance == null) {
        
        //     instance = this; 
        // }
        // else {
        //     Destroy (gameObject);
        // }
        cachedRectTransform = ActiveDispBar.GetComponent<RectTransform>();
        active_x = cachedRectTransform.sizeDelta.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBar(float par){
        float SetPar = active_x * par;
        cachedRectTransform.sizeDelta = new Vector2(SetPar, cachedRectTransform.sizeDelta.y);        
    }
}
