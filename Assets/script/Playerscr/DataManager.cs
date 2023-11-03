using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
  public int MaxHp;
  public int CurrentHp;
  public int CurrenHeath;
  public int Maxheath;
  public int Coin;

  public int Skinindex;
  public bool[] skinunlock = new bool[4];

  public bool[] levelUnlock = new bool[4];



}

public class DataManager : MonoBehaviour
{
  public static DataManager instance;

  void Awake()
  {
    if (instance != null)
    {
      Destroy(this, .1f);

    }
    else
    {
      instance = this;
      DontDestroyOnLoad(this);
    }
  }

  public Data data = new Data();

  public void LoadDataInJsonFlie()
  {


    string file = "DataPlayer.json";
    string filepath = Path.Combine(Application.persistentDataPath, file);
    if (!File.Exists(filepath))
    {
      File.WriteAllText(filepath, "");
      data = JsonUtility.FromJson<Data>(File.ReadAllText(filepath));
    }
    data = JsonUtility.FromJson<Data>(File.ReadAllText(filepath));
  }
  public void SaveDataToJson()
  {
    string file = "DataPlayer.json";
    string filepath = Path.Combine(Application.persistentDataPath, file);
    string json = JsonUtility.ToJson(data, true);
    File.WriteAllText(filepath, json);
  }





  private void Update()
  {
    if (Input.GetKey(KeyCode.C))
    {
      SaveDataToJson();
    }
    if (Input.GetKey(KeyCode.H))
    {
      LoadDataInJsonFlie();
    }
  }

}
