
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
    
   

    protected override void CreateEnemyDeath()
    {
        base.CreateEnemyDeath();
    }


  

    

}


