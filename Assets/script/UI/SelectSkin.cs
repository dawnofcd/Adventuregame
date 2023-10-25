using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
public class SelectSkin : MonoBehaviour
{
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private int[] priceForSkin;
    [SerializeField] private int skin_Id;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;

    [SerializeField] int totalCoins;


    string coin;
    void Start()
    {
        SetupSkinInfo();
    }

    private void SetupSkinInfo()
    {
        GetUserData();
        skinPurchased[0] = true;
        skinPurchased[1] = false;
        skinPurchased[2] = false;
        skinPurchased[3] = false;
        bankText.text = totalCoins.ToString();
        OnGetInfoSkin();
    }


    void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetInfoBank, OnError);
    }

    private void OnError(PlayFabError error)
    {
        throw new NotImplementedException();
    }

    private void OnGetInfoBank(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Currency"))
        {
            coin = result.Data["Currency"].Value;
        }
        int.TryParse(coin, out totalCoins);
    }

    private void OnGetInfoSkin()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
       {
           if (result.Data != null)
           {
               for (int i = 0; i < skinPurchased.Length; i++)
               {
                   if (result.Data.ContainsKey("skinPurchase" + i) && result.Data["skinPurchase" + i].Value == "1")
                   {
                       skinPurchased[i] = true;
                   }
               }
               selectButton.SetActive(skinPurchased[skin_Id]);//true
               buyButton.SetActive(!skinPurchased[skin_Id]);//false
               if (!skinPurchased[skin_Id])
                   buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skin_Id];
                   anim.SetInteger("skinid", skin_Id);
           }
       }, error => { /* Handle error */ });

    }


    public bool EnoughMoney()
    {

        if (totalCoins >= priceForSkin[skin_Id])
        {
            totalCoins = totalCoins - priceForSkin[skin_Id];
            SendTotalsToServer();
            bankText.text = totalCoins.ToString();
            return true;
        }

        //AudioManager.instance.PlaySFX(0);

        return false;
    }


    public void NextSkin()
    {
        Audiomanager.instance.ButtonClick();
        skin_Id++;
        if (skin_Id > 3)
            skin_Id = 0;
        SetupSkinInfo();
    }

    public void PreviousSkin()
    {
        Audiomanager.instance.ButtonClick();

        skin_Id--;
        if (skin_Id < 0)
            skin_Id = 3;
        SetupSkinInfo();
    }

    public void Buy()
    {
        if (EnoughMoney())
        {
            skinPurchased[skin_Id] = true;

            if (skinPurchased[skin_Id])
            {
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
                {
                    Data = new Dictionary<string, string>
                    {
                        { "skinPurchase" + skin_Id, "1" }
                    }
                }, result => { /* Handle success */ }, error => { /* Handle error */ });

            }
            SetupSkinInfo();

        }
        else
            Debug.Log("Not Enogh Money");
    }

    public void Select()
    {
        Audiomanager.instance.ButtonClick();

        PlayerManager.choosenSkinId = skin_Id;
    }

    public void SwitchSelectButton(GameObject newButton)
    {
        selectButton = newButton;
    }
    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        selectButton.SetActive(false);
    }

    public void SendTotalsToServer()
    {

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Currency",totalCoins.ToString()}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnUserDataUpdated, OnError);

    }

    private void OnUserDataUpdated(UpdateUserDataResult result)
    {

    }
}

