using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Radish : Enemy
{
  
   [SerializeField] Transform endpoint;
   [SerializeField] Transform startpoint;
   
   private float maxpos=5f;
   private float poschange=0f;
   bool checkcollidehead;
   float timewalkon=0f;
   
   protected override void Start()
    {
         base.Start();
         invicinble = true;
       
    }
// nếu quái vật đang ở trên trời thì nó sẽ bay
   private void Update() 
   {
        CollisionChecks();
        
    
        if(!groundDetected)
        {
          invicinble=true;
          Flip();
        }
       else
       {
         invicinble=false;
         rb.bodyType=RigidbodyType2D.Dynamic;
         walkAround();
       }

        if(checkcollidehead)
        {
         // FlyDown();
        }
        
        
   }




// void FlyUp()
// {
  
//   rb.bodyType=RigidbodyType2D.Kinematic;
//   if(poschange>=maxpos)
//   {
//     rb.velocity=new Vector2(rb.velocity.x,0f);
//   }
//   else
//   {
//     rb.velocity= new Vector2(rb.velocity.x,5f);

//     poschange+=5f*Time.deltaTime;
//   }
// }

// void FlyDown()
// {
//     timewalkon=0f;
//     if(poschange>maxpos)
//   {
//     rb.velocity=new Vector2(rb.velocity.x,0f);
//   }
//   else
//   {
//     rb.velocity= new Vector2(rb.velocity.x,-5f);
//     poschange+=5f*Time.deltaTime;
//   }
// }

 void Flyup()
 {
    transform.position= Vector2.MoveTowards(transform.position,endpoint.position, speed*Time.deltaTime);
 }

 void Flydown()
 {
   transform.position= Vector2.MoveTowards(transform.position,startpoint.position, speed*Time.deltaTime);
 }
protected  override void walkAround()
{
   float timewalk=6f;
  if(timewalkon<timewalk)
  { 
    base.walkAround();
  }
  if(timewalkon>timewalk)
  {
    Flyup();  
  }
  timewalkon+=Time.deltaTime;
}
   
}

