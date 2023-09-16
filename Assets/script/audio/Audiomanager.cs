using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;
    public AudioSource _musicSound;
     public AudioSource _gfx;
    
    public AudioClip effect, RunSound, JumpSound;
    
    private void Awake()
    {
        instance = this;
    }


    public void ButtonClick()
    {
     _gfx.PlayOneShot(effect);
    }
    

}


