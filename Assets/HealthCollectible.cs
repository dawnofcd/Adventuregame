using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private GameObject pickUpFX;
    private void OnTriggerEnter2D(Collider2D collison) 
    {
        if(collison.GetComponent<Player>() != null)
        {   
            
            PlayerReceiver.instance.HeathReceiver();
           
            if(pickUpFX != null)
            {
                GameObject newPickUpFX = Instantiate(pickUpFX, transform.position, transform.rotation);
                Destroy(newPickUpFX,.5f);
            }
            
            Destroy(gameObject);
        }
    }
}
