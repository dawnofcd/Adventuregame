
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;




public class InGame_Ui : VollumController
{
    [SerializeField] private bool gamePause;
    // private bool gameDead;

    [Header("Menu Game obj")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject defeatUI;
    [SerializeField] private GameObject endLevelUI;
    [SerializeField] Animator transition;




    //[Header("Controlls")]
    //[SerializeField] private VariableJoystick joystick;
    //[SerializeField] private Button jumpButton;


    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI endCoinsText;
    [SerializeField] private TextMeshProUGUI scoreText;


    private int HpScore;
    private int timerScore;

    public int score;

    //[Header("Volume")]

    protected override void Awake()
    {
        base.Awake();
        if (PlayerManager.instance.inGameUI == null)
        {
            PlayerManager.instance.inGameUI = this;
        }

    }
    void Start()
    {
        // PlayerManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SwitchUI(inGameUI);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIfNotPause();
        }
    }
    public void AssignPlayerControlls(Player player)
    {
        // player.joystick = joystick;

        // jumpButton.onClick.RemoveAllListeners();
        // jumpButton.onClick.AddListener(player.JumpButton);   
    }

    public void PauseButton() => CheckIfNotPause();


    private bool CheckIfNotPause()
    {
        if (!gamePause)
        {
            gamePause = true;
            Time.timeScale = 0;
            SwitchUI(pauseUI);
            return true;
        }
        else
        {
            gamePause = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
            return false;
        }

    }

    public void OnDeath()
    {

        SwitchUI(defeatUI);
        Time.timeScale = 0f;
    }

    public void DisableDeath()
    {
        SwitchUI(inGameUI);
        Time.timeScale = 1f;
    }

    public void OnLevelFinished()
    {
        score = ScoreRate();
        endCoinsText.text = "Coins: " + PlayerReceiver.instance.Coin;
        endTimerText.text = "Your time: " + Gamemanager.instance.timer.ToString("00") + " s";
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + score);
        Audiomanager.instance.PlayFinish();
        SwitchUI(endLevelUI);
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

        if (uiMenu == inGameUI)
        {
            // joystick.gameObject.SetActive(true);
            // jumpButton.gameObject.SetActive(true);
        }

    }

    public void LoadMainMenu()
    {
        //AudioManagerInGame.instance.PlaySFXInGame(6);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReloadCurrentLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void LoadNextLevel(string level)
    {   
        int totalScore = PlayerPrefs.GetInt("TotalScore");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        int NextSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LevelManager.instance.UpdateUnlockLevel(NextSceneIndex);
        StarManager.instance.SendStarToServer(level);
        PlayerReceiver.instance.UpCoinToServer();
        SendLeaderBoardScore(totalScore);
    }

    protected override void SetGfxVolume(float value)
    {
        base.SetGfxVolume(value);
    }



    protected override void SetMusicVolume(float value)
    {
        base.SetMusicVolume(value);
    }



    int ScoreRate()
    {
        int StarScore = PlayerReceiver.instance.Star * 100;
        if (PlayerReceiver.instance.CurrenHeath < 3)
        {
            HpScore = 200;
        }
        if (PlayerReceiver.instance.CurrenHeath == 3)
        {
            HpScore = 300;
        }

        if (Gamemanager.instance.timer <= 30f)
        {
            timerScore = 1000;
        }
        else
        {
            timerScore = 200;
        }

       int  score = StarScore + HpScore + timerScore;
       return score;
    }

    

    public void ResetScore()
    {
        PlayerPrefs.SetInt("TotalScore",0);
    }


    public void SendLeaderBoardScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Hights Score", // Tên thống kê đã tạo
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdated, OnError);
    }

    private void OnLeaderboardUpdated(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("thanhcong");
    }

    private void OnError(PlayFabError error)
    {
        // Xử lý lỗi nếu có
    }

}

