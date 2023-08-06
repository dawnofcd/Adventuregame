using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;
    public AudioSource _musicSound, effectSound;
    private void Awake()
    {
       Audiomanager.instance = this;
    }
   
    void Start()
    {
        Playsound();
    }    
    public void Playsound()
    {
        _musicSound.Play();
    }    
    void RunSound()
    {

    }

    void JumpSound()
    {

    }    
}
