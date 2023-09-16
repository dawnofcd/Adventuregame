using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class fallingPlat : MonoBehaviour
{
   [SerializeField]private Animator ani;
   [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float TimePedal;
    private float currentTime;
   private float ForceAmount=10f;
   private Vector2 forceDirect=new Vector2(0,1);

   void Timepedal()
   {
    if(currentTime<TimePedal)
    {
      currentTime+=Time.deltaTime;
    }
    else currentTime=0;
   }
   void LateUpdate()
   {
    
   }
   void OnCollisionEnter2D(Collision2D other)
   {
      if(other.gameObject.GetComponent<Player>())
      {
        if(currentTime==0)
       { ani.SetBool("Isfalling", true);
        rb.AddForce(force: ForceAmount*forceDirect, ForceMode2D.Force);
       }
       else
       {
         rb.velocity= new Vector2(rb.velocity.x, 0);
       }

      }
   }
}
