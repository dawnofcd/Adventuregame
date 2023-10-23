using System.Collections;
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

   void Start()
   {
     SetupSkinInfo();
   }

    private  void SetupSkinInfo()
    {
        GetUserData();
        skinPurchased[0] = true;
        skinPurchased[1] = false;
        skinPurchased[2] = false;
        skinPurchased[3] = false;

        for (int i = 0; i < skinPurchased.Length; i++)
        {
             bool skinUnlocked = PlayerPrefs.GetInt("skinPurchase" + i) == 1;
           

            if(skinUnlocked)
            skinPurchased[i] =  true;
        }

        selectButton.SetActive(skinPurchased[skin_Id]);//true
        buyButton.SetActive(!skinPurchased[skin_Id]);//false
        
        if(!skinPurchased[skin_Id])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text ="Price: " + priceForSkin[skin_Id];

           anim.SetInteger("skinid",skin_Id);
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
       bankText.text = result.Data["Currency"].Value;
    }

    }

    public bool EnoughMoney()
    {
       int totalCoins = PlayerPrefs.GetInt("TotalCoinsCollected");
      

        if(totalCoins >= priceForSkin[skin_Id]) 
        {
            totalCoins = totalCoins - priceForSkin[skin_Id];

            PlayerPrefs.SetInt("TotalCoinsCollected", totalCoins);
            
            
           // AudioManager.instance.PlaySFX(2);
            return true;
        }

     //   AudioManager.instance.PlaySFX(0);

        return false;
    }


    public void NextSkin()
    {
       Audiomanager.instance.ButtonClick();
        skin_Id ++;

        if(skin_Id > 3)
        skin_Id = 0;
        SetupSkinInfo();
    }

    public void PreviousSkin()
    {
        Audiomanager.instance.ButtonClick();

        skin_Id --;

        if(skin_Id <0)
         skin_Id = 3;

        SetupSkinInfo();

    }

    public void Buy()
    {
        if(EnoughMoney())
        {    
            //PlayerPrefs.SetInt("skinPurchase" + skin_Id, 1);
            // Dictionary<string,object> SkinPurchasetoSever=  new Dictionary<string, object>{{"skinPurchase"+ skin_Id.ToString(),1}};
            // await CloudSaveService.Instance.Data.ForceSaveAsync(SkinPurchasetoSever);
            skinPurchased[skin_Id] = true;
            SetupSkinInfo();

        }
        else
            Debug.Log("Not Enogh Money");

    }

    public void Select()
    {
        Audiomanager.instance.ButtonClick();

        PlayerManager.choosenSkinId= skin_Id;
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


}

