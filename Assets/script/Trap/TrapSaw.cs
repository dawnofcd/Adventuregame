using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;

    private int movePointIndex;
    private float cooldownTimer;
    [SerializeField] private float cooldown;

    private void Start()
    {
        anim = GetComponent<Animator>();


    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        bool isWorking = cooldownTimer < 0;
        anim.SetBool("isWorking", isWorking);


        if (isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.15f)
        {
            Flip();
            cooldownTimer = cooldown;
            movePointIndex ++;
            
            if(movePointIndex >= movePoint.Length)
            {
                movePointIndex =0;
            }
            
        }


    }

    private void Flip()
    {
        transform.localScale = new Vector3 (1, transform.localScale.y * -1);
    }
    

}
