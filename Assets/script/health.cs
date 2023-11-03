using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<PlayerReceiver>().CurrenHeath+=1;
            Destroy(this.gameObject);
        }
    }
}
