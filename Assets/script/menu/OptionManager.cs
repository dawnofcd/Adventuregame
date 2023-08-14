using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class OptionManager : MonoBehaviour

{
    public GameObject buttonMusic;
    public GameObject buttonSound;
    public GameObject buttonMute;
    public GameObject buttonMuteSound;
   [SerializeField] private List<GameObject> Character;
    private int IndexChar;
    public Slider SliderBGM, SliderGfx;
    public AudioSource _musicSound, effectSound, Jumpsound, Runsound, WallclimSound;


   
    //private void //Awake() //loi khong tim thay object khi no setactive false 
    // solution gắn thẳng object vào script 
    //{
    //    buttonMusic = GameObject.Find("musicbutton");
    //    buttonSound = GameObject.Find("soundbutton");
    //    buttonMute = GameObject.Find("mutebutton");
    //    buttonMuteSound = GameObject.Find("mutesoundbutton");
    //}

   
    public void Start()
    {
        dataPlayermanager.instance.Load();
        Character[IndexChar].SetActive(true);
    }

    public void SliderVolumeMusic()
    {
       
       _musicSound.volume = SliderBGM.value;
        PlayerCrl.instance.dataPlayer.BgmVolume= _musicSound.volume;
        if (_musicSound.volume > 0)
        {
            buttonMusic.SetActive(true);
            buttonMute.SetActive(false);
        }
        else if (_musicSound.volume == 0)
        {
            buttonMusic.SetActive(false);
            buttonMute.SetActive(true);
        }
      
    }
    public void SliderVolumeGfx()
    {

        effectSound.volume = SliderGfx.value;
        PlayerCrl.instance.dataPlayer.GfxVolume = effectSound.volume;

        if (effectSound.volume > 0)
        {
            buttonSound.SetActive(true);
            buttonMuteSound.SetActive(false);
        }
        else if (effectSound.volume == 0)
        {
            buttonSound.SetActive(false);
            buttonMuteSound.SetActive(true);
        }
    }



    public void Onmusic()
    {

        buttonMusic.SetActive(true);
        _musicSound.volume = 50;
        SliderBGM.value = 0.5f;
        buttonMute.SetActive(false);
        Audiomanager.instance.ButtonClick();
    }
    public void Offmusic()
    { 
        buttonMusic.SetActive(false);
        _musicSound.volume = 0;
        SliderBGM.value = 0f;
        buttonMute.SetActive(true);
        Audiomanager.instance.ButtonClick();
    }

     
    public void OnGfx()
     {
        buttonSound.SetActive(true);
        PlayerCrl.instance.dataPlayer.GfxVolume= 50;
        SliderGfx.value = 0.5f;
        buttonMuteSound.SetActive(false);
        Audiomanager.instance.ButtonClick();
    }
    
    
    public void OffGfx() 
    { 
        buttonSound.SetActive(false);
        PlayerCrl.instance.dataPlayer.GfxVolume = 0;
       
        SliderGfx.value = 0;
        buttonMuteSound.SetActive(true);
        Audiomanager.instance.ButtonClick();
    }

    public void Rightclickselect()
    {
        Character[IndexChar].SetActive(false);
        IndexChar--;
        if (IndexChar<0)
         IndexChar = Character.Count-1;
        Character[IndexChar].SetActive(true);
        Audiomanager.instance.ButtonClick();
    }

    public void LeftClickSelect()
    {
        Character[IndexChar].SetActive(false);
        IndexChar++;
        if (IndexChar == Character.Count)
        IndexChar = 0;
        Character[IndexChar].SetActive(true);
        Audiomanager.instance.ButtonClick();
    }

   
}
