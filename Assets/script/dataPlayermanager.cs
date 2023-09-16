
using System.Xml.Linq;
using System.Diagnostics;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class dataMenuGame
{ 
    public int indexCharacter;
    public float GfxVolume;
    public float BgmVolume;
}

[System.Serializable]
public class dataGamePlayer
{
   int currentHP;
   int CurrentHealth;
   int Coins;
}
public class dataPlayermanager : MonoBehaviour
{
   public dataMenuGame Data= new dataMenuGame();
// public OptionManager OptionManager;
    [SerializeField] private VollumController[] vollumController;
    private void Start()
    {
    //    OptionManager=GetComponent<OptionManager>();


        for (int i = 0; i < vollumController.Length; i++)
        {
           // vollumController[i].GetComponent<VollumController>().SetupVollumeSlider();
        }

        AudioManager.instance.PlayRandomBGM();

       // LoadOptiondata();
    }

    public void LoadOptiondata()
    {
        string file = "SaveOption.json";
        string filepath = Path.Combine(Application.persistentDataPath, file);
        if (!File.Exists(filepath)) { 
            File.WriteAllText(filepath, "");
             Filldata();
             Data = JsonUtility.FromJson<dataMenuGame>(File.ReadAllText(filepath));
         }
         Data = JsonUtility.FromJson<dataMenuGame>(File.ReadAllText(filepath));
    }

    public void Filldata()
    {
        // OptionManager.IndexChar=Data.indexCharacter;
        // OptionManager._effect.volume = Data.GfxVolume;
        // OptionManager._musicSound.volume = Data.BgmVolume;  
    }

    public void SaveOptiondata()
    {
        // Data.indexCharacter= OptionManager.IndexChar;
        // Data.GfxVolume = OptionManager._effect.volume;
        // Data.BgmVolume = OptionManager._musicSound.volume;
    }

    public void saveOption()
    {
        string file = "SaveOption.json";
        string filepath = Path.Combine(Application.persistentDataPath, file);
        string json = JsonUtility.ToJson(Data,true);
        File.WriteAllText(filepath, json);
        SaveOptiondata();
    }


   

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
          saveOption(); 
        }    
    }
    public void SaveOption()
        {

        }

        public void SaveOnflat()
        {

        }

    }

