using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Gamemanager : MonoBehaviour
{
     public static Gamemanager instance;

     public float timer;

    public TextMeshProUGUI   timerText;
    private bool endGame;

    private void Awake()
    {
        if (instance == null)
        {
           instance = this;
        }    

    }



    void Update()
    {
        timer += Time.deltaTime; 
        int roundedTimer = Mathf.RoundToInt(timer);
        timerText.text = "Timer: " + roundedTimer.ToString() + "s";
    }


}
