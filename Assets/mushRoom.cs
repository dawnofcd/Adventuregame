
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class mushRoom : Enemy
{
    [SerializeField] Transform checkdeadpos;
    [SerializeField] Transform playerPos;
    [SerializeField] protected LayerMask whatIsplayer;
    public bool Checkdead;
    public float playercheckdistance;
    protected override void Start()
    {
        base.Start();
      
    }

    private void Update()
    {

        walkAround();
        CollisionChecks();
        anim.SetFloat("XVelocity", rb.velocity.x);
        checkDead();
    }
    float distance()
    {
        Vector2 checkDeadPos2D = new Vector2(checkdeadpos.position.x, checkdeadpos.position.y);
        Vector2 playerPos2D = new Vector2(playerPos.position.x, playerPos.position.y);
        playercheckdistance = Vector2.Distance(checkDeadPos2D, playerPos2D);
        return playercheckdistance;
    }    
    
    void checkDead()
    { Checkdead = Physics2D.Raycast(origin: checkdeadpos.position, direction: Vector2.up, distance() , layerMask: whatIsplayer); 
        if(Checkdead ) { Debug.Log("true"); }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkdeadpos.position, new Vector2(playerPos.position.x + playercheckdistance, playerPos.position.y));
    }


    protected override void CreateEnemyDeath()
    {
        base.CreateEnemyDeath();
    }


    public override void Damage()
    {
       base.Damage();
    }

    public override void  DestroyMe()
    {
       base.DestroyMe();
    }

    

}


