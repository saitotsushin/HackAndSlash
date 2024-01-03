using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum GAMESTATUS
    {
        PLAY,
        GAMEOVER,
    }
public class GManager : MonoBehaviour
{
    static public GManager instance;
    public GAMESTATUS GAMESTATUS;
    
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
    public void GameOver(){
        Debug.Log("ゲームオーバーです");
        GAMESTATUS = GAMESTATUS.GAMEOVER;
        UIManager.instance.GameOver();
    }
}
