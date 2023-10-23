using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint: MonoBehaviour
{
    [SerializeField] Transform resPoint;
    Animator anim;
    private void Awake() 
    {
    }

    void Start()
    {

        anim=GetComponent<Animator>();
        PlayerManager.instance.spawnPoint = resPoint;
        PlayerManager.instance.RespawnPlayer();

    }

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
        }
    }

}
