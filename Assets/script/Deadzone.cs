using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadzone : MonoBehaviour
{
    public Transform retrans;
 void OnTriggerEnter2D(Collider2D other)
 {
    other.gameObject.GetComponent<Player>().transform.position=retrans.position;
    other.gameObject.GetComponent<Player>().CurrenHeath-=3;
     
 }
}
