using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Diagnostics;

public class LevelManager : MonoBehaviour
{
  [SerializeField] Button[] Level;
  
  [SerializeField]  ButtonWithStar []buttonWithStar;
  public int []TotalStarLevel;
  [SerializeField] int UnLockLevel;
  public static LevelManager instance;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
  }

  void Start()
  {
  
     GetUnlockLevel();
     

  }

  

  public void SetupButtonLvGame()
  {
    Level = new Button[transform.childCount];

    for (int i = 0; i < Level.Length; i++)
    {
      Level[i] = transform.GetChild(i).GetComponent<Button>();
      Level[i].GetComponentInChildren<Text>().text = "Level" + " " + (i + 1).ToString();

      if (i + 1 > UnLockLevel)
      {
        Level[i].interactable = false;
      }
    }
  }

  

  public void GetUnlockLevel()
  {
    PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnLevelUnlockDataGet, OnError);
  }

  public void UpdateUnlockLevel(int newUnlockLevel)
  {

    var request = new UpdateUserDataRequest
    {
      Data = new Dictionary<string, string>
        {
            {"UnlockLevel", newUnlockLevel.ToString()}
        }
    };

    PlayFabClientAPI.UpdateUserData(request, OnUserDataUpdated, OnError);
  }

  private void OnUserDataUpdated(UpdateUserDataResult result)
  {
    // Dữ liệu đã được cập nhật thành công
  }

  public void OnLevelUnlockDataGet(GetUserDataResult result)
  {
    if (result.Data != null && result.Data.ContainsKey("UnlockLevel"))
    {
      string newUnlockLevel = result.Data["UnlockLevel"].Value;
      int.TryParse(newUnlockLevel, out UnLockLevel);
      SetupButtonLvGame();
    
    }
    else
    {
        if (UnLockLevel == 1)
    {
      Level = new Button[transform.childCount];
      for (int i = 0; i < Level.Length; i++)
      {
        Level[i] = transform.GetChild(i).GetComponent<Button>();
        Level[i].GetComponentInChildren<Text>().text = "Level" + " " + (i + 1).ToString();
        if (i + 1 > UnLockLevel)
        {
          Level[i].interactable = false;
        }
        
      }
    }
    }

  for(int i=0; i<Level.Length;i++)
   { 
    if (result.Data != null && result.Data.ContainsKey("level"+ i))
    {
      string newTotalStar = result.Data["level"+ i].Value;
      int.TryParse(newTotalStar, out TotalStarLevel[i]);
      buttonWithStar[i].StarRating(TotalStarLevel[i]);
      //PlayerPrefs.SetInt("star"+i, TotalStarLevel[i]);
    }
    else
    {

      //PlayerPrefs.SetInt("star"+i, 0);
    }
   }
  }


  private void OnError(PlayFabError error)
  {
    // Xử lý lỗi khi cập nhật dữ liệu
  }

  public void LoadScene(int Level)
  {
    Audiomanager.instance.ButtonClick();
    SceneManager.LoadScene(Level);
  }

}
