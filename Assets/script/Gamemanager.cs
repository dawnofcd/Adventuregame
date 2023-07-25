using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance; 
    private void Awake()
    {
        Gamemanager.instance = this;
    }
    void Savegame()
    {
    }
    void loadgame()
    {

    }    
    void choiceChacracter ()
    {
    }

   
}
