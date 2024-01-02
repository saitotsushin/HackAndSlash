using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    static public GManager instance;
    
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
        LoadGameSetting();
    }

    public void LoadGameSetting(){
        ItemManager.instance.LoadEquipmentSlot();
    }
}
