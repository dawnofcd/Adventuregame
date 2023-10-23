using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using Unity.Services.CloudSave;


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
    public float  transitionTime = 1f;

    
    //[Header("Controlls")]
    //[SerializeField] private VariableJoystick joystick;
    //[SerializeField] private Button jumpButton;


    //[Header("Text Components")]
    //[SerializeField] private TextMeshProUGUI  timerText;
   // [SerializeField] private TextMeshProUGUI endTimerText;
   // [SerializeField] private TextMeshProUGUI endBestTimeText;
   // [SerializeField] private TextMeshProUGUI endCoinsText;
    
    //[Header("Volume")]
   
    protected  override void Awake()
    {
        base.Awake();
        if(PlayerManager.instance.inGameUI==null)
       {
          PlayerManager.instance.inGameUI = this;
       }
        
    }
     void Start()
    {
       // PlayerManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        
        Time.timeScale  = 1f;
        SwitchUI(inGameUI);
    }

   

   private void Update() 
   {
        if(Input.GetKeyDown(KeyCode.Escape))
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
        if(!gamePause)
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

    // private bool CheckIfNotDead()
    // {
    //     if(!gameDead)
    //     {
    //         gameDead = true;
    //         Time.timeScale = 0;
    //         SwitchUI(defeatUI);
    //         return true;
    //     }
    //     else
    //     {
    //         gameDead = false;
    //         Time.timeScale = 1;
    //         SwitchUI(inGameUI);
    //         return false;
    //     }

    // }

    public void OnDeath()
    {  
        
        SwitchUI(defeatUI);
        Time.timeScale=0f;
    }

    public void DisableDeath()
    {
         SwitchUI(inGameUI);
         Time.timeScale=1f;
    }

    public void OnLevelFinished()
    {
       // endCoinsText.text = "Coins: " + PlayerManager.instance.coins;
        //endTimerText.text = "Your time: " + GameManager.instance.timer.ToString("00") +  " s";
      //  endBestTimeText.text = "Best time: " + PlayerPrefs.GetFloat("Level" + GameManager.instance.levelNumber + "BestTime",999).ToString("00") + " s";
        SwitchUI(endLevelUI);
    }
    private void UpdateInGameInfo()
    {
        //timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

        if(uiMenu == inGameUI)
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
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       int NextSceneIndex=SceneManager.GetActiveScene().buildIndex;
       LevelManager.instance.UpdateUnlockLevel(NextSceneIndex);
       StarManager.instance.SendStarToServer(level);
       PlayerReceiver.instance.UpCoinToServer();     
        //transition.SetBool("transition",true);
    }

    protected override void Setgfxvolume(float value)
    {
        base.Setgfxvolume(value);
    }



    protected override void Setmusicvolume(float value)
    {
        base.Setmusicvolume(value);
    }

}

