using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    static public TestManager instance;
    public Text TestText;
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
