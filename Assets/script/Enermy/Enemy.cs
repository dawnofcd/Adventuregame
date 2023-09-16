using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Danger
{

   [SerializeField] protected Animator anim;
    protected Rigidbody2D rb;
    protected int facingDirection = -1;
    [SerializeField] public float health;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatToIgnore;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    protected bool wallDetected;
    [SerializeField]protected bool groundDetected;
    protected RaycastHit2D playerDectection;

    protected Transform player;
    [SerializeField] public bool invicinble;

    [Header("Move info")]
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime = 2;
    protected float idleTimeCounter;

    protected bool canMove = true;
    protected bool aggresive;

    [Header("DeathSpawn")]
    [SerializeField] private GameObject EnemyDeathPrefab;
    [SerializeField] protected Transform EnemyDeathOrigin;



    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("FindPlayer",0 , .5f);
        if (groundCheck == null)
           groundCheck = transform;
        if (wallCheck == null)
           wallCheck = transform;

    }

    private void FindPlayer()
    {
       if (player != null)
           return;

    //    if (PlayerCrl.instance.transform != null)
    //        player = PlayerCrl.instance.transform;
    }

    protected virtual void walkAround()
    {

        if (idleTimeCounter <= 0 && canMove)
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, 0);


        idleTimeCounter -= Time.deltaTime;

        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }
// không đụng thì là true, đụng là
    public virtual void Damage()
    {
        health--;

        if (!invicinble)
        {   
                anim.SetBool("gotHit", true);
            if(health <=0)
            {
                anim.SetBool("gotHit", true);
                canMove = false;
                DestroyMe();
            }
        }
       
    }

    private void StopHitAnimation()
    {
        anim.SetBool("gotHit", false);
        
    }

    public virtual void  DestroyMe()
    {
        Destroy(gameObject);
        CreateEnemyDeath();
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        playerDectection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 100, ~whatToIgnore);
    }

    protected virtual void OnDrawGizmos()
    {
        if (groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        if (wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDectection.distance * facingDirection, wallCheck.position.y));
                
        }
    }

    protected virtual void CreateEnemyDeath()
    {
        GameObject newEnemyDeath = Instantiate(EnemyDeathPrefab, EnemyDeathOrigin.transform.position, EnemyDeathOrigin.transform.rotation);
        newEnemyDeath.GetComponent<Enemy>();
        Destroy(newEnemyDeath, 3f);

    }

   

}
