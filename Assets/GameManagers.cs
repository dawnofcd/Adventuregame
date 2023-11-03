using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;

    public float timer;

    public TextMeshProUGUI timerText;

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

public class Gamemanagers
{
}