using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RADISH : Enemy
{
    private RaycastHit2D groundBelowDetected;
    private RaycastHit2D groundAboveDetected;

    [Header("Radish specifics")]
    [SerializeField] private float ceillingDistance;
    [SerializeField] private float groundDistance;
    
    [SerializeField] private float aggroTime;
                     private float aggroTimeCounter;

    [SerializeField] private float flyForce;
    

    [Header("Radish SpawnLeafs")]
    [SerializeField] private GameObject leafs1Prefab;
    [SerializeField] private GameObject leafs2Prefab;
    [SerializeField] private Transform leafs1Origin;
    [SerializeField] private Transform leafs2Origin;
    

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        aggroTimeCounter -= Time.deltaTime;

        if(aggroTimeCounter < 0 && !groundAboveDetected)
        {
            rb.gravityScale = 1;
            aggresive = false;
        }
        
        if(!aggresive)
        {
            if (groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0, flyForce);
            }
        }
        else
        {
            if(groundBelowDetected.distance <= 1.25f)
                walkAround();

        }

        CollisionChecks();
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("aggrisive", aggresive);
            
        
    }

    public override void Damage()
    {
        if(!aggresive)
        {
            aggroTimeCounter = aggroTime;
            rb.gravityScale = 12;
            aggresive = true;
            CreateLeafs();

        }    
        else
            base.Damage();

    }
    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, ceillingDistance, whatIsGround);
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, whatIsGround);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceillingDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundDistance));
    }

    private void CreateLeafs()
    {
        GameObject newLeafs1 = Instantiate(leafs1Prefab, leafs1Origin.transform.position, leafs1Origin.transform.rotation);
        newLeafs1.GetComponent<RADISH>();
        Destroy(newLeafs1, 2.5f);

        GameObject newLeafs2 = Instantiate(leafs2Prefab, leafs2Origin.transform.position, leafs2Origin.transform.rotation);
        newLeafs2.GetComponent<RADISH>();
        Destroy(newLeafs2, 2.5f);
        
    }


}
