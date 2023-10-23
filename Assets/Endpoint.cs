using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endpoint : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] InGame_Ui UiEndmap;

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>())
        {
            if(other.transform.position.x > transform.position.x)
            {
                anim.SetTrigger("touch");
            }

            if(other.transform.position.x < transform.position.x)
            {
                anim.SetTrigger("touch");
            }
            StartCoroutine(Delay());
            
        }
    }

IEnumerator Delay()
{
   yield return new WaitForSeconds(1f);
   UiEndmap.OnLevelFinished();
   Time.timeScale=0f;
     
}
}
