
using UnityEngine;

public class DusktraiController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rbPlayer;
    [SerializeField] private ParticleSystem _moveOnGround;
     [SerializeField] private ParticleSystem _wallSliDing;
      
     
    float _counter;
   
     void FixedUpdate()
    {
        MoveOnGround();
        OnWall();
    }



void MoveOnGround()
{
     _counter+=Time.deltaTime;
        if(Mathf.Abs(_rbPlayer.velocity.x)>0.5f && Player.isGrounded|| Player.isWallDetected)
        {
            if(_counter>0.2f)
            {
                _moveOnGround.Play();
               
                _counter=0f;
            }
        }

}

void OnWall()
{
      if(Player.isWallSliding)  
        {
             _moveOnGround.Stop();
             _wallSliDing.Play();
        }
}


}