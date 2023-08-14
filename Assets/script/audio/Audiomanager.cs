using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audiomanager : OptionManager
{
    public static Audiomanager instance;
    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Playsound()
    {
        _musicSound.Play();
    }

    public void ButtonClick()
    {
        effectSound.Play();
    }
    void RunSound()
    {
        Runsound.Play();
    }

    void JumpSound()
    {
        Jumpsound.Play();
    }

}


