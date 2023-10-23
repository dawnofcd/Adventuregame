using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class CoinManager : MonoBehaviour
{
    [SerializeField] private Transform[] coinPosition;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private bool randomCoin;
    
    private int CoinIndex;

    void Start()
    {
        coinPosition = GetComponentsInChildren<Transform>();
        //int levelNumber = GameManager.instance.levelNumber;

        for (int i = 1; i < coinPosition.Length; i++)
        {
            GameObject newCoin = Instantiate(coinPrefab, coinPosition[i]);

            if(randomCoin)
            {
                CoinIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(CoinsType)).Length);
                newCoin.GetComponent<Coin>().CoinSetup(CoinIndex);

            }
            else
            {
                newCoin.GetComponent<Coin>().CoinSetup(CoinIndex);
                CoinIndex++;

                if(CoinIndex  > Enum.GetNames(typeof(CoinsType)).Length)
                    CoinIndex = 0;
            }
            
            coinPosition[i].GetComponent<SpriteRenderer>().sprite = null;
            

        }
           // int totalAmountOfCoins = PlayerPrefs.GetInt("Level" + levelNumber + "TotalCoins");

           // if(totalAmountOfCoins != coinPosition.Length  -1 )
              //  PlayerPrefs.SetInt("Level" + levelNumber + "TotalCoins",  coinPosition.Length -1);

    }

}
