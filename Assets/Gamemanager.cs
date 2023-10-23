using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

      public int choosenSkinId;

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

        PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime");

    }

}
