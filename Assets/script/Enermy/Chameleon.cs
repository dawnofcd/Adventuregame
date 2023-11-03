using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon : Enemy
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _boxAttack;
    [SerializeField] private float _distanceAttack;
                     bool _attack;
    [SerializeField] private LayerMask _whatIsPlayer;
                    float dametime=0.5f;  
                    float damecounter;
  protected override  void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
         CollisionChecks();
        AnimationController();
        CheckDistanceAttack();
        if(!_attack)
        {
             walkAround();
            _boxAttack.SetActive(false); 
            anim.SetBool("Attack",false);
            damecounter=dametime;
            return;
        }

        else
        {    if(damecounter<=0)
            _boxAttack.SetActive(true); 
            anim.SetBool("Attack",true);
        }
        
            if(!groundDetected)
            {
                Flip();

            }
        damecounter-=Time.deltaTime;
    }
     
     void CheckDistanceAttack()
     {
       _attack= Physics2D.Raycast(_boxAttack.transform.position,Vector2.right * facingDirection,_distanceAttack,_whatIsPlayer);
     }

    void AnimationController()
   {    
        anim.SetBool("gotHit", invicinble);
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
