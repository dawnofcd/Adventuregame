using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdamager : MonoBehaviour
{
    [SerializeField] int Damager; 
   private void  OnCollisionEnter2D(Collision2D other) 
   {
    
     if(other.gameObject.GetComponent<Player>()!=null)
     {
        PlayerReceiver.instance.TakeDame(Damager);
        other.gameObject.GetComponent<Player>().KnockBack(transform);
     }
   }
     
}
