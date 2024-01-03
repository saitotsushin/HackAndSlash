using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHunger : MonoBehaviour
{
    public int damageCount = 10;
    public int setDamageCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hunger(){
        Player.instance.mPlayerStatus.StatusHunger -= 0.001f;
        if(Player.instance.mPlayerStatus.StatusHunger <= 0){
            Player.instance.mPlayerStatus.StatusHunger = 0;
            HungerPoint.instance.textComponent.text = "0";
            setDamageCount++;
            if(setDamageCount > damageCount){
                Player.instance.Damage(1);
                setDamageCount = 0;
            }
        }else{
            int roundedUpInteger = Mathf.CeilToInt(Player.instance.mPlayerStatus.StatusHunger);
            HungerPoint.instance.textComponent.text = roundedUpInteger.ToString();
        }
    }
}
