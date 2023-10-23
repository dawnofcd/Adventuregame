using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : Enemy
{
    
    public Vector3 initialPosition;
    public float moveSpeed ;
    public float returnSpeed ;
    public Vector3 targetPosition;

    private bool movingTowardsTarget = true;

    private float timecounter;
  
  protected override  void Start()
    {
      base.Start();
    }

   
    void Update()
    {

        anim.SetBool("gotHit",invicinble);
          if (movingTowardsTarget)
        {
             timecounter=idleTime;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;

            // Kiểm tra nếu đã đến gần điểm đích
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
               
                
                Flip();
                movingTowardsTarget = false;
                rb.velocity = Vector3.zero;
               }
                

        }
        else
        {   
            timecounter=idleTime;
            Vector3 returnDirection = (initialPosition - transform.position).normalized;
            rb.velocity = returnDirection * returnSpeed;

            if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            {
               
                Flip();
                movingTowardsTarget = true;
                rb.velocity = Vector3.zero;
             
               
            }
        }
        
    }
    }
    

   

