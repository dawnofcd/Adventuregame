using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [HideInInspector] public GameObject currentPlayer;

    [Header("Player Info")]
    [SerializeField] public Transform spawnPoint;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject deathFx;

    [Header("CameraShake FX")]
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float forceMultiplier;
                     public InGame_Ui inGameUI;
    public static int choosenSkinId;

    private void  Awake()
    {
       instance=this;
    }

     public void OnFalling()
    {
        KillPlayer();
        

         PlayerReceiver.instance.CurrenHeath=0;
         PlayerReceiver.instance.CurrentHp=0;

//        int difficulty = Gamemanager.instance.difficulty;
        // if(HaveEnoughCoins())
        // {
        //     PearmanentDeath();
        // }

        // if(difficulty < 3)
        // {

        //     //if(difficulty >1)
        //     //HaveEnoughCoins();
        // }
        // {
        // else
        // }

    }

  
    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x * facingDir, shakeDirection.y ) * forceMultiplier;
        impulse.GenerateImpulse();
    }

    public void RespawnPlayer()
    {
        if(currentPlayer == null)
        {
            inGameUI.DisableDeath();
            currentPlayer =Instantiate(playerPrefab, spawnPoint.position, transform.rotation);
            Audiomanager.instance.PlayReSpawn();
            PlayerReceiver.instance.CurrenHeath=2;
            PlayerReceiver.instance.CurrentHp=100;
        }

    }


    public void KillPlayer()
        {
        if(currentPlayer != null)
        
        {  Audiomanager.instance.PlayDeath();
            GameObject newDeathFx = Instantiate(deathFx, currentPlayer.transform.position, currentPlayer.transform.rotation);
            Destroy(currentPlayer,0.1f);
            Destroy(newDeathFx, 0.1f);
            StartCoroutine(ActiveUiDeath());
        }
        else
        {
            return;
        }
        }

IEnumerator ActiveUiDeath()
{
   yield return new WaitForSeconds(1f);
   inGameUI.OnDeath();
}




}
