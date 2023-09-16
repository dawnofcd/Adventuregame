using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{  
    public static GameManager instance;
    public GameObject Menupause;
   
    public int difficulty;
    private void Awake()
    {
        if (instance == null)
        {
            GameManager.instance = this;
            
         
        }
       
    }

    private void Start()
    {
        if(difficulty == 0)
            difficulty = PlayerPrefs.GetInt("GameDifficulty");


    }


    public void PauseGame()
    {
        Menupause.SetActive(true);
        Time.timeScale=0f;
    }
    public void ResumeGame()
    {
        Menupause.SetActive(false);
        Time.timeScale=1f;
    }
    public void Refresh()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale=1f;

    }
    
    public void BacktoHome()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    void Savegame()
    {
    }
    void loadgame()
    {

    }    
    void choiceChacracter ()
    {
    }

   
}
