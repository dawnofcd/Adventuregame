using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SignInManager : MonoBehaviour
{

    [SerializeField] Text messageText;

    [SerializeField] InputField UsernameInput;
    [SerializeField] InputField emailInput;
    [SerializeField] InputField passwordRegisInput;
    [SerializeField] InputField passwordInput;
    [SerializeField] InputField confirmInput;
    [SerializeField] GameObject uiRegister;
    [SerializeField] GameObject uiLogin;
    [SerializeField] GameObject uiReset;

    [SerializeField] InputField UsernameResInput;



    public void SwitchRegister()
    {
        uiRegister.SetActive(true);
        uiLogin.SetActive(false);
        uiReset.SetActive(false);
    }


    public void BackToLogin()
    {
        uiRegister.SetActive(false);
        uiLogin.SetActive(true);
        uiReset.SetActive(false);
    }

    public void ResetUi()
    {
        uiRegister.SetActive(false);
        uiLogin.SetActive(false);
        uiReset.SetActive(true);
    }

    public void RegisterBtn()
    {

        if (emailInput.text == "" || passwordRegisInput.text == "" || confirmInput.text == "" || UsernameResInput.text == "")
        {
            messageText.text = "Please enter enough information!";
            return;
        }

        if (passwordRegisInput.text != confirmInput.text)
        {
            messageText.text = "Confirm password not correct!";
            return;
        }

        if (passwordRegisInput.text.Length < 6)
        {
            messageText.text = "Password must have 6 characters!";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Username = UsernameResInput.text,
            Password = passwordRegisInput.text,
            RequireBothUsernameAndEmail = true,
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);

    }

    private void OnError(PlayFabError error)
    {
        messageText.text = error.ToString();
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = " register Success !";
    }



    public void LoginBtn()
    {
        var request = new LoginWithPlayFabRequest
        {

            Username = UsernameInput.text,
            Password = passwordInput.text,
        
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);

    }
  
   
   public void ChangeDisplayNameToUserName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = UsernameInput.text,
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdated, OnError);
    }

   
    private void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        
    }

    IEnumerator LoadToHome()
    {
         SceneManager.LoadScene(1);
         ChangeDisplayNameToUserName();
         yield return new WaitForSeconds(2f);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "LogIn!";

        StartCoroutine(LoadToHome());
    }

    void OnPassWordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "email recovery send your mail";
    }


    public void ResetPasswordBtn()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "905F5",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPassWordReset, OnError);
    }
   
}
