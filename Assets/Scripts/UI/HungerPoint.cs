using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerPoint : MonoBehaviour
{
    static public HungerPoint instance;
    public Text textComponent;
    void Awake ()
    {
        if (instance == null) {
        
            instance = this; 
        }
        else {
            Destroy (gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
