
using UnityEngine;


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
