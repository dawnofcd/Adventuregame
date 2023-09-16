using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private int bgmIndex;
    
    private void  Awake()
    {
        if(instance = null)
            instance = this;
        else
            Destroy(this.gameObject);


    }


    // Update is called once per frame
    void Update()
    {
        if(!bgm[bgmIndex].isPlaying)
        {
            bgm[bgmIndex].Play();
        }
    }

    public void PlaySFX(int sfxToPlay) //random SFx
    {
        if(sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].pitch = Random.Range(0.85f, 1.15f); 
            sfx[sfxToPlay].Play(); 
        }



    }

    public void PlayBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }

        bgm[bgmToPlay].Play();


    }

    public void PlayRandomBGM() //random BGM
    {
        bgmIndex = Random.Range(0, bgm.Length);

        PlayBGM(bgmIndex);



    }






}
