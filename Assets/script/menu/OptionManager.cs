// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.UI;

// [System.Serializable]
// public class OptionManager : Audiomanager

// {

//     public GameObject buttonMusic;
//     public GameObject buttonSound;
//     public GameObject buttonMute;
//     public GameObject buttonMuteSound;
//    [SerializeField] private List<GameObject> Character;
//     public int IndexChar;
//     public Slider SliderBGM, SliderGfx;
   
  

   
//     //private void //Awake() //loi khong tim thay object khi no setactive false 
//     // solution gắn thẳng object vào script 
//     //{
//     //    buttonMusic = GameObject.Find("musicbutton");
//     //    buttonSound = GameObject.Find("soundbutton");
//     //    buttonMute = GameObject.Find("mutebutton");
//     //    buttonMuteSound = GameObject.Find("mutesoundbutton");
//     //}

   
//     public void Start()
//     {  
//         Character[IndexChar].SetActive(true);



//     }

    

   
//     public void SliderVolumeMusic()
//     {
       
//        _musicSound.volume = SliderBGM.value;
//         if (_musicSound.volume > 0)
//         {
//             buttonMusic.SetActive(true);
//             buttonMute.SetActive(false);
//         }
//         else if (_musicSound.volume == 0)
//         {
//             buttonMusic.SetActive(false);
//             buttonMute.SetActive(true);
//         }
      
//     }
//     public void SliderVolumeGfx()
//     {

//         _musicSound.volume = SliderGfx.value;

//         if (mus.volume > 0)
//         {
//             buttonSound.SetActive(true);
//             buttonMuteSound.SetActive(false);
//         }
//         else if (_effect.volume == 0)
//         {
//             buttonSound.SetActive(false);
//             buttonMuteSound.SetActive(true);
//         }
//     }



//     public void Onmusic()
//     {

//         buttonMusic.SetActive(true);
//         _musicSound.volume = 50;
//         SliderBGM.value = 0.5f;
//         buttonMute.SetActive(false);
//         ButtonClick();
//     }
//     public void Offmusic()
//     { 
//         buttonMusic.SetActive(false);
//         _musicSound.volume = 0;
//         SliderBGM.value = 0f;
//         buttonMute.SetActive(true);
//         ButtonClick();
//     }

     
//     public void OnGfx()
//      {
//         buttonSound.SetActive(true);
//         SliderGfx.value = 0.5f;
//         buttonMuteSound.SetActive(false);
//         ButtonClick();
//     }
    
    
//     public void OffGfx() 
//     { 
//         buttonSound.SetActive(false);
//         SliderGfx.value = 0;
//         buttonMuteSound.SetActive(true);
//         ButtonClick();
//     }

//     public void Rightclickselect()
//     {
//         Character[IndexChar].SetActive(false);
//         IndexChar--;
//         if (IndexChar<0)
//          IndexChar = Character.Count-1;
//         Character[IndexChar].SetActive(true);
//         ButtonClick();
//     }

//     public void LeftClickSelect()
//     {
//         Character[IndexChar].SetActive(false);
//         IndexChar++;
//         if (IndexChar == Character.Count)
//         IndexChar = 0;
//         Character[IndexChar].SetActive(true);
//         ButtonClick();
//     }

   
// }
