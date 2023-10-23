using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterName : MonoBehaviour
{
    public string NamePlayer;
    public InputField NameInput;

   public void EnterNamePlayer()
   {
        NamePlayer=NameInput.text;
        PlayerPrefs.SetString("NamePlayer", NamePlayer);
        Debug.Log(PlayerPrefs.GetString("NamePlayer"));
        SceneManager.LoadScene(1);
   }
}
