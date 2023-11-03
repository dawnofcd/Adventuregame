using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : Enemy
{

    [SerializeField] GameObject _bulletprefabs;
    [SerializeField] Transform _origin;
    [SerializeField] LayerMask _whatIsplayer;
    private float _nextSpawnTime=0f;
    private float  _spawnInterval=1f;
    [SerializeField] private float _playercheckdis;
    [SerializeField] public static float _facing;
   // [SerializeField] Animator ani;
    
 protected override void Start()
    {
        base.Start();
    }

 



private void Update() {
    FindPlayer();
    AutoChangeDir();
}

void AutoChangeDir()
{
   
    if (Mathf.Approximately(this.transform.eulerAngles.y, 180f)) // phai kiem tra ky
{
    _facing = 1f;
}
if (Mathf.Approximately(this.transform.eulerAngles.y, 0f))
{
    _facing = -1f;
}

}

   void FindPlayer()
   {
     bool playerDetect= Physics2D.Raycast(_origin.position,Vector2.right * _facing,_playercheckdis,layerMask: _whatIsplayer);

     if(playerDetect)
     {
         SpawnBullet();
     }else
     {
        anim.SetBool("Playerdetect",false);
     }
   }
   
   
    void SpawnBullet()
    {
        if (Time.time >= _nextSpawnTime)
    {
        // Thực hiện spawn
        anim.SetBool("Playerdetect",true);
        GameObject newbullet=  Instantiate(_bulletprefabs, _origin.position, Quaternion.identity);
        newbullet.transform.parent=this.transform;
        // Cập nhật thời điểm spawn tiếp theo
        _nextSpawnTime = Time.time + _spawnInterval;
    }

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
