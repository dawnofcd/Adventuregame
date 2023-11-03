using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
   [SerializeField] Animator anim;
   
   void Start()
   {
      anim= GetComponent<Animator>();;
   }
   
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.GetComponent<Player>() != null)
       {
           if(PlayerReceiver.instance.Star >= 3)
           {
            return;
           }
                                    
            PlayerReceiver.instance.Star++;
            Destroy( this.gameObject,.1f );
            StarManager.instance.CalculatorStar();
       }

    }
}
