using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrl : MonoBehaviour
{
   
    public static PlayerCrl instance;
    // public dataPlayermanager dataPlayermanager;
   
    
    private void Awake()
    {
        
        
        // dataPlayermanager=GetComponent<dataPlayermanager>();
        PlayerCrl.instance = this;
    }

}
