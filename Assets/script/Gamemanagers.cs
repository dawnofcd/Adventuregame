using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanagers : MonoBehaviour
{  
    public static Gamemanagers instance;
    public int levelNumber;
   
    public int difficulty;
    private void Awake()
    {
        if (instance == null)
        {
           instance = this;
            DontDestroyOnLoad(this);
            
        }
        else
        {
             Destroy(this,.1f);
        }
       
    }




    private void Start()
    {
        if(difficulty == 0)
        difficulty = PlayerPrefs.GetInt("GameDifficulty");
    }



   
}
