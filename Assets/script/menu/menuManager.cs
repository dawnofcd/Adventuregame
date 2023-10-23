using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.SharedModels;
using PlayFab.ClientModels;

public class menuManager : MonoBehaviour
{
     void Start()
    {
      GetPlayerName();   
    }
    [SerializeField] Text userNameDisplay;
    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        Audiomanager.instance.ButtonClick();
        uiMenu.SetActive(true);

    }

    public void Logout()
    {
     PlayFabClientAPI.ForgetAllCredentials();
     SceneManager.LoadScene(0);
    }


   
   // Hàm này gọi API của PlayFab để lấy tên người chơi
    private void GetPlayerName()
    {
        var request = new GetPlayerProfileRequest
        {
            // Sử dụng ID người chơi hoặc tên người chơi (tùy chọn)
            PlayFabId = PlayFabSettings.staticPlayer.PlayFabId
        };

        PlayFabClientAPI.GetPlayerProfile(request, OnPlayerProfileReceived, OnError);
    }

  
    private void OnPlayerProfileReceived(GetPlayerProfileResult result)
    {
      if(result.PlayerProfile.DisplayName==null)
    {
        return;
    }
      else userNameDisplay.text = "WellCome: " + result.PlayerProfile.DisplayName.ToString();
    }

    // Hàm được gọi khi có lỗi xảy ra
    private void OnError(PlayFabError error)
    {
        Debug.LogError("Error getting player profile: " + error.ErrorMessage);
    }
}








