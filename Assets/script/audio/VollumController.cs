using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VollumController : MonoBehaviour
{
  [SerializeField] public Slider _sliderMusic;

  [SerializeField] public Slider _sliderGfx;

  [SerializeField] AudioMixer _mixer;

  public const string MixserMusic = "mixermusic";
  public const string MixserGfx = "mixergfx";



  protected virtual void Awake()
  {

    Loadvollume();
    _sliderGfx.onValueChanged.AddListener(SetMusicVolume);
    _sliderMusic.onValueChanged.AddListener(SetGfxVolume);
  }

  protected virtual void SetMusicVolume(float value)
  {
    _mixer.SetFloat(name: MixserMusic, value: Mathf.Log10(value) * 20);

  }

  protected virtual void SetGfxVolume(float value)
  {
    _mixer.SetFloat(name: MixserGfx, value: Mathf.Log10(value) * 20);

  }


   public void Savevollume()
  {
    PlayerPrefs.SetFloat(MixserMusic, _sliderMusic.value);
    PlayerPrefs.SetFloat(MixserGfx, _sliderGfx.value);
  }

 void  Loadvollume()
  {
    float musicvollume = PlayerPrefs.GetFloat(MixserMusic, 1f);
    float gfxvollume = PlayerPrefs.GetFloat(MixserGfx, 1f);
    _sliderGfx.value = musicvollume;
    _sliderMusic.value = gfxvollume;
    _mixer.SetFloat(MixserGfx, Mathf.Log10(gfxvollume) * 20);
    _mixer.SetFloat(MixserMusic, Mathf.Log10(musicvollume) * 20);
  }



  public void MuteMusicVoLume()
  {
    _mixer.SetFloat(MixserMusic, -80f);
    _sliderMusic.value = 0f;
  }

  public void MuteGfx()
  {
    _mixer.SetFloat(MixserGfx, -80f);
    _sliderGfx.value = 0f;
  }
}
