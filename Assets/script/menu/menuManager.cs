using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
   
   public void Loadnewgame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void onClickExit()
    {
        Application.Quit();
    }
}
