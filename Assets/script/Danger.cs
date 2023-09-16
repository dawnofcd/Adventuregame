using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    protected virtual  void OnTriggerEnter2D(Collider2D other)
    {
 {
    if (other.gameObject.GetComponent<Player>())
    {   
        other.gameObject.GetComponent<Player>().KnockBack(transform);
    }
 }
        
    }

}
