using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : Enemy
{
    [SerializeField] private float agroSpeed;
    [SerializeField] private float shockTime;
                    private float shockTimeCounter;


   protected override void Start()
   {
        base.Start();
        invicinble = true;
   }

   private void Update()
   {    
        CollisionChecks();
        AnimationController();
        if(!playerDectection)
        {
            walkAround();
            return;
        }

        if(playerDectection.collider.GetComponent<Player>()!= null && playerDectection)
        {
            aggresive = true;

        }

        if(!aggresive)
        {
            walkAround();

        }
        else //aggresive = true
        {   
            // speed +=agroSpeed * facingDirection * Time.deltaTime;

            if(!groundDetected)
            {
                aggresive = false;
                Flip();

            }
            rb.velocity = new Vector2(agroSpeed * facingDirection, rb.velocity.y);

            if(wallDetected && invicinble)
            {
                invicinble = false;
                shockTimeCounter = shockTime;
              
            }

            if(shockTimeCounter <=0 && !invicinble)
            {
                invicinble = true;
                Flip();
                aggresive = false;

            }

            shockTimeCounter -= Time.deltaTime;


        }
   }

   void AnimationController()
   {    
        anim.SetBool("invincible", invicinble);
        anim.SetFloat("XVelocity", rb.velocity.x);
   }


   protected override void  OnTriggerEnter2D(Collider2D other)  
  {
    base.OnTriggerEnter2D(other);
    if (other.gameObject.GetComponent<Player>()!=null)
    {   
       PlayerReceiver.instance.TakeDame(Damemage);
    }
  }
  
}
    