
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   [SerializeField] Rigidbody2D _rb;
   [SerializeField] List<GameObject> _pieceprefabs;
    GameObject _newpecei;
    void move()
    {
      _rb.velocity=new Vector2(10f*Plant._facing,0f);
      
    }
    void Update()
    {
        move();
    }

   void CreatePiece()
   {
     Destroy(this.gameObject,0.1f);
    foreach(GameObject x in _pieceprefabs)
    {
      float randompos= Random.Range(0f,1f);
      _newpecei=Instantiate(x,this.transform.position+new Vector3(randompos,0f,0f),Quaternion.identity);
      _newpecei.transform.Rotate(new Vector3(0,0,180f));
       Destroy(_newpecei,1f);
    }
   }

    void OnTriggerEnter2D(Collider2D other)
   {

     if(other.tag=="ground" || other.tag=="Player")
     {
      if(other.gameObject.GetComponent<Player>()!=null)
     { 
       PlayerReceiver.instance.TakeDame(50);
       other.gameObject.GetComponent<Player>().KnockBack(transform);
     }
       CreatePiece();
     }
   }

   
}
