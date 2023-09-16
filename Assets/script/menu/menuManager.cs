using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
  //  [SerializeField] private VollumController[] vollumController;
    private void Start()
    {
        
    }
    // public void SwitchMenuTo(GameObject uiMenu)
    // {
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         transform.GetChild(i).gameObject.SetActive(false);
    //     }

    //     AudioManager.instance.PlaySFX(1);
    //     uiMenu.SetActive(true);

    // }

    // public void SetGameDifficulty(int i) =>GameManager.instance.difficulty = i;
    


   public void Loadnewgame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
        Time.timeScale= 1.0f;
    }

    public void onClickExit()
    {
        Application.Quit();
    }
}
