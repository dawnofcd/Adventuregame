
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class menuManager : MonoBehaviour
{
     void Start()
    {
      GetPlayerName();   
    }
    [SerializeField] Text userNameDisplay;
   
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private Transform leaderboardContent;
     
    [SerializeField] private GameObject layout;

    private bool hasFetchedLeaderboard = false;
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

     public void GetLeaderBoardData()
    {
         if (!hasFetchedLeaderboard)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Hights Score", 
            StartPosition = 0,
            MaxResultsCount = 10 // Số lượng người chơi hàng đầu bạn muốn hiển thị
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardDataReceived, OnError);
    
         hasFetchedLeaderboard = true;
     }

    }


    private void OnLeaderboardDataReceived(GetLeaderboardResult result)
    {
       
    
        foreach (var player in result.Leaderboard)
        {
            GameObject leaderboardEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            leaderboardEntry.transform.parent=layout.transform;
            TextMeshProUGUI playerNameText = leaderboardEntry.transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI playerScoreText = leaderboardEntry.transform.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI playerPlaceText = leaderboardEntry.transform.Find("playerPlaceText").GetComponent<TextMeshProUGUI>();
            playerNameText.text = player.DisplayName.ToString();
            playerScoreText.text = player.StatValue.ToString();
            playerPlaceText.text=(player.Position+1).ToString();
        }
    
    }

    // Xử lý lỗi
    private void OnError(PlayFabError error)
    {
        Debug.LogError("PlayFab Error: " + error.ErrorMessage);
    }
}










