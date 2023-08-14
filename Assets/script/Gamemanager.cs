using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{  
    public static Gamemanager instance;
    public GameObject Menupause;
   
    
    private void Awake()
    {
        if (instance == null)
        {
            Gamemanager.instance = this;
            
         
        }
       
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
        Destroy(GameObject.Find("Audiomanager"));
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
