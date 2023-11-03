
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mushRoom : Enemy
{
    protected override void Start()
    {
        base.Start();

    }

    private void Update()
    {

        walkAround();
        CollisionChecks();
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


