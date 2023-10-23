using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadzone : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.GetComponent<Player>() != null)
        {
              
            PlayerManager.instance.OnFalling();
        }  
    }
}
