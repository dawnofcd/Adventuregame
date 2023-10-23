
using UnityEngine;

public class Rock : Enemy
{
   [SerializeField] GameObject _rockToSpawn=null;
  

   
   protected override void Start()
    {
        base.Start();
      
    }

    void Update()
    {
        walkAround();
        CollisionChecks();
       // anim.SetFloat("XVelocity", rb.velocity.x);
        if(isdeath)
        { SpawnRock(); }
        else{ return;}
        if(!groundDetected)
        {
            rb.velocity=new Vector2(rb.velocity.x, -10f);
        }

    }
     
    

    

    void SpawnRock()
    {
            float minposx=-2f;
            float maxposx=2f;
            GameObject newRock= Instantiate(_rockToSpawn,this.transform.position+new Vector3(minposx,0f,0f),Quaternion.identity); 
            GameObject newRock2= Instantiate(_rockToSpawn,this.transform.position+new Vector3(maxposx,0f,0f),Quaternion.identity); 
            isdeath=false;
          
    }
    
}
