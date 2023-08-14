
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // giup covert ve class dataplayer ve string

public class dataPlayermanager : MonoBehaviour
{
   public static dataPlayermanager instance ;

    
    private void Awake()
    {
        instance = this ;
    }

    private void Start()
    {
        Load();
    } 
    public void Load()
    {
        
            string file = "Save.json";
            string filepath = Path.Combine(Application.persistentDataPath, file);
            if (!File.Exists(filepath)) { File.WriteAllText(filepath, ""); }
            PlayerCrl.instance.dataPlayer = JsonUtility.FromJson<dataPlayer>(File.ReadAllText(filepath));
        Debug.Log("load");
           
    }
    
      
      
    public void save()
    {
        string file = "Save.json";
        string filepath = Path.Combine(Application.persistentDataPath, file);
        string json= JsonUtility.ToJson(PlayerCrl.instance.dataPlayer,true);
        File.WriteAllText(filepath,json);

        Debug.Log("file save at path : " +filepath);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            save();
        }    
    }
    public void SaveOption()
        {

        }

        public void SaveOnflat()
        {

        }

    }

