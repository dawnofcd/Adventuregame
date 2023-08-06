using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OptionManager : MonoBehaviour

{
   public GameObject buttonMusic;
    public GameObject buttonSound;
   public GameObject buttonMute;
   public GameObject buttonMuteSound;
   

    //private void //Awake() //loi khong tim thay object khi no setactive false 
    // solution gắn thẳng object vào script 
    //{
    //    buttonMusic = GameObject.Find("musicbutton");
    //    buttonSound = GameObject.Find("soundbutton");
    //    buttonMute = GameObject.Find("mutebutton");
    //    buttonMuteSound = GameObject.Find("mutesoundbutton");
    //}
    
    
       
    

   public void Onmusic()
    {

      buttonMusic.SetActive(true);
      buttonMute.SetActive(false);
        
    }
    public void Offmusic()
    {

        buttonMusic.SetActive(false);
        buttonMute.SetActive(true);
    }


    public void Onsound()
     {
        buttonSound.SetActive(true);
        buttonMuteSound.SetActive(false);
     }

    public void Offsound() 
    {
        buttonSound.SetActive(false);
        buttonMuteSound.SetActive(true);
    }
}
