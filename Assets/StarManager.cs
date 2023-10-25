using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
public class StarManager :MonoBehaviour
{
  [SerializeField] GameObject []Stars= new GameObject[3];
 
 public static StarManager instance;

void Awake()
{
  instance=this;
}

void Start()
{
  for (int i = 0; i < Stars.Length; i++)
    {
          Stars[i].SetActive(false);
    }
}
  public void CalculatorStar()
{
    for (int i = 0; i < Stars.Length; i++)
    {
        if (i == PlayerReceiver.instance.Star - 1)
       {
            Stars[i].SetActive(true);
       }
    }
   
}

 public void SendStarToServer(string level)
  {

    int currentTotalStar =PlayerReceiver.instance.Star;

     var request = new UpdateUserDataRequest
    {
        Data = new Dictionary<string, string>
        {
            {level, currentTotalStar.ToString()}
        }
    };
    PlayFabClientAPI.UpdateUserData(request, OnUserDataUpdated, OnError);
  }

    private void OnUserDataUpdated(UpdateUserDataResult result)
    {
       Debug.Log("succsess");
    }

    private void OnError(PlayFabError error)
    {
        
    }
}
