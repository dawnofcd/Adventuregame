using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayerReceiver : MonoBehaviour
{
    public static PlayerReceiver instance;
    public int MaxHp;
    public int CurrentHp;
    public int CurrenHeath;
    public int Maxheath;
    public  int Star;
    public  int Coin;

    public TextMeshProUGUI CoinUi;
    [SerializeField] List<GameObject> heath= null;
    void Awake()
    {    
         instance=this;
         heath.Add(GameObject.Find("uiheath_1"));
         heath.Add(GameObject.Find("uiheath_2"));
         heath.Add(GameObject.Find("uiheath_3")); 
    }
   
    private void Start()
    {        
            
    }
   public void CaculatorHp(ref int currentheath, int maxheath,ref int Hp, int maxhp)
    {
       
        if (Hp < 0)
    {
        currentheath--;
        Hp = maxhp;
    }
    else if (currentheath >= 3)
    {
        currentheath = maxheath;
    }
    else if (currentheath < 0)
    {
        Hp = 0;
    }

    heathstate(  currentheath);
    }

   public  void heathstate(  int  heathClamp)
    {
        if (heathClamp==1)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(false);
            this.heath[2].SetActive(false);

        }
        else if (heathClamp == 2)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(true);
            this.heath[2].SetActive(false);
        }
        else if (heathClamp == 3)
        {
            this.heath[0].SetActive(true);
            this.heath[1].SetActive(true);
            this.heath[2].SetActive(true);
        }
        else if (heathClamp == 0)
        {
            this.heath[0].SetActive(false);
            this.heath[1].SetActive(false);
            this.heath[2].SetActive(false);
            PlayerManager.instance.OnFalling();
        }
        
    }   
     public void TakeDame(int Damemager)
    {
        CurrentHp  -=Damemager;
    } 
    
     public void CoinReceiver()
    {
        Coin+=1;
    }

    public void HeathReceiver()
    {
        CurrenHeath+=1;
    }
   private void  FixedUpdate()
   {
      CoinUi.text=Coin.ToString();
      CaculatorHp(ref CurrenHeath,Maxheath, ref CurrentHp,MaxHp);
      Hpbar.instance.UpdateHpBar(CurrentHp,MaxHp);
   }

  
  public void UpCoinToServer() 
  {
        int CoinCurrent = 0;   
        CoinCurrent += Coin;
       var request = new UpdateUserDataRequest
    {
      Data = new Dictionary<string, string>
        {
            {"Currency",CoinCurrent.ToString()}
        }
    };

    PlayFabClientAPI.UpdateUserData(request, OnUserDataUpdated, OnError);
  }

    private void OnError(PlayFabError error)
    {
        throw new NotImplementedException();
    }

    private void OnUserDataUpdated(UpdateUserDataResult result)
    {
       // throw new NotImplementedException();
    }
}
