using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint: MonoBehaviour
{
    [SerializeField] private Animator anim;
    private void Awake() {
        anim=GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
             anim.SetBool("IsCollided", true);
        }
    }

     void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
             anim.SetBool("IsCollided", false);
        }
    }

     void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
             anim.SetBool("IsCollided", false);
        }
    }


}
