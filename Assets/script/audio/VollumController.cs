using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VollumController : MonoBehaviour
{
   [SerializeField]  Slider _sliderMusic;

   [SerializeField]  Slider _sliderGfx;

   [SerializeField] AudioMixer _mixer;

   public const string MixserMusic= "mixermusic";
   public const string MixserGfx= "mixergfx";

void Awake()
{
    _sliderGfx.onValueChanged.AddListener(Setmusicvolume);
    _sliderMusic.onValueChanged.AddListener(Setgfxvolume);
}

void Start()
{
   Loadvollume();
}

 void Setmusicvolume(float value)
 {
    _mixer.SetFloat(name:MixserMusic ,value: Mathf.Log10(value)*20);
 }

void Setgfxvolume(float value)
 {
    _mixer.SetFloat(name:MixserGfx ,value: Mathf.Log10(value)*20);
 }

 
 public void Savevollume()
 {
   PlayerPrefs.SetFloat(MixserMusic,_sliderMusic.value);
   PlayerPrefs.SetFloat(MixserGfx,_sliderGfx.value);
 }

 void Loadvollume()
 {
   float musicvollume= PlayerPrefs.GetFloat(MixserMusic,1f);
   float gfxvollume=PlayerPrefs.GetFloat(MixserGfx,1f);
   _sliderGfx.value=musicvollume;
   _sliderMusic.value=gfxvollume;
   _mixer.SetFloat(MixserGfx,Mathf.Log10(gfxvollume)*20);
   _mixer.SetFloat(MixserMusic,Mathf.Log10(musicvollume)*20);
 }
  
  

public void Mutevolume()
{
    _mixer.SetFloat(MixserMusic,-80f);
    _sliderMusic.value=0f;

}

public void Onvolume()
{
    _mixer.SetFloat(MixserMusic,-4f);
    _sliderMusic.value=0.5f;
}
}
