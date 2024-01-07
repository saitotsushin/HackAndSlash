using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorArrow : MonoBehaviour
{
    public GameObject CursorArrowObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CursorShow(bool _isShow){
        CursorArrowObj.SetActive(_isShow);
    }
}
