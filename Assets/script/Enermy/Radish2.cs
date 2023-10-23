
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Radish2 : Enemy
{
  [SerializeField] private bool _isFly;
  [SerializeField] private int _countFlip;
  [SerializeField] private float _distanceCheckHead;
  [SerializeField] private bool hit;
  [SerializeField] private Transform _head;
  [SerializeField] private LayerMask _player;
  [SerializeField] private List <GameObject> _leafPrefab;
  protected override void Start() 
  {
    
    base.Start();

  }

  private void Update() 
  {
    CollisionChecks();
    checkcollidehead();
    AnimateControl();
    // checkcollidehead();
    if (_isFly)
    {  
        canMove=false;
        invicinble=true;
    }
    else
    { 
          canMove= true;
  
    }
      WalkOnGround();
  }

  void WalkOnGround()
  {
    
    if(canMove && idleTimeCounter<=0)
    {
      rb.velocity= new Vector2(speed*facingDirection,rb.velocity.y);
    }
    else rb.velocity=Vector2.zero;

    if(!groundDetected && !_isFly||wallDetected)
    {
        Flip();
        _countFlip++;
        idleTimeCounter=idleTime;
    }
    if(groundDetected)
    {
      _isFly=false;
      invicinble=false;
      rb.gravityScale=3f;
    }
  
  if(_countFlip==2)
  {
    canMove=false;
    _isFly=true;
    FlyUp();
  }
  if(hit)
  {
    Fall();

  }
    idleTimeCounter-=Time.deltaTime;
}


  void checkcollidehead()
  {
    hit = Physics2D.Raycast(_head.transform.position,Vector2.up,_distanceCheckHead,  _player);
  }
  void Flying()
  {   
    _isFly=true;
      rb.bodyType= RigidbodyType2D.Kinematic;
  }
    void Fall()
    {  rb.gravityScale=10f;
    rb.bodyType= RigidbodyType2D.Dynamic;
        Dropleaf();
    }
  void FlyUp()
  {
    rb.AddForce(Vector2.up*1.5f,ForceMode2D.Impulse);
    rb.gravityScale=0f;
    
    // _countflip=0;
    Invoke("resetcount",3f);
  }

  void resetcount()
  {
    _countFlip=0;
      rb.bodyType= RigidbodyType2D.Kinematic;
  }

  void Dropleaf()
  {
    foreach(GameObject x in _leafPrefab)
    {
      GameObject newleaf=  Instantiate(x,_head.transform.position, quaternion.identity); 
      newleaf.transform.parent=this.transform;
      Destroy(newleaf,1f);
    }
  }

  void AnimateControl()
  {
      anim.SetBool("Fly",_isFly);
      anim.SetFloat("XVelocity",rb.velocity.x);
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
