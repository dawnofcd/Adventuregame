using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audiomanager : MonoBehaviour
{
   public static Audiomanager instance;
   public AudioSource _musicSound;
   public AudioSource _gfx;
   public AudioClip effect, RunSound, JumpSound, Death, WallJump, KickEnemy, ReSpawn, Finish, Knocked, PickItem;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         //DontDestroyOnLoad(this);
      }
      else
      {
         //Destroy(this.gameObject,.1f);
      }
   }



   private void Start()
   {
      // _musicSound[0].Play();
   }


   //    public void PlayRandomSound()
   //    {
   //      for(int i=1;i<_musicSound.Length;i++)
   //         {
   //             int randomMusic= Random.Range(1,_musicSound.Length);
   //             if (randomMusic==i)
   //             {
   //                 _musicSound[i].Play();
   //             }
   //             _musicSound[0].Stop();
   //         }
   //    }
   public void ButtonClick()
   {
      _gfx.PlayOneShot(effect);
   }

   public void PlayPickUp()
   {
      _gfx.PlayOneShot(RunSound);
   }

   public void PlayJumpSound()
   {
      _gfx.PlayOneShot(JumpSound);
   }

   public void PlayKicked()
   {
      _gfx.PlayOneShot(KickEnemy);
   }

   public void PlayWallsilde()
   {
      _gfx.PlayOneShot(WallJump);
   }

   public void PlayReSpawn()
   {
      _gfx.PlayOneShot(ReSpawn);
   }


   public void PlayKnocked()
   {
      _gfx.PlayOneShot(Knocked);
   }

   public void PlayDeath()
   {
      _gfx.PlayOneShot(Death);
   }

   public void PlayFinish()
   {
      _gfx.PlayOneShot(Finish);
   }
}


