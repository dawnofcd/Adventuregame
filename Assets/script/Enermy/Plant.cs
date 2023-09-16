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
    Findplayer();
    Autochangedir();
}

void Autochangedir()
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

   void Findplayer()
   {
     bool playerdetect= Physics2D.Raycast(_origin.position,Vector2.right * _facing,_playercheckdis,layerMask: _whatIsplayer);

     if(playerdetect)
     {
         SpawnBullet();
     }else
     {
        anim.SetBool("Playerdetect",false);
     }
   }
   
   private void OnDrawGizmos()
   {
      Gizmos.DrawLine(_origin.position,new Vector2(_origin.position.x+_playercheckdis*_facing,this.transform.position.y));
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
   
}
