using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;
    protected int facingDirection = -1;

    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected LayerMask whatToIgnore;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    protected bool wallDetected;
    protected bool groundDetected;
    protected RaycastHit2D playerDectection;

    protected Transform player;
    [HideInInspector] public bool invicinble;

    [Header("Move info")]
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime = 2;
    protected float idleTimeCounter;

    protected bool canMove = true;
    protected bool aggresive;

    [Header("DeathSpawn")]
    [SerializeField] private GameObject EnemyDeathPrefab;
    [SerializeField] private Transform EnemyDeathOrigin;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       // FindPlayer();

        //if (groundCheck == null)
        //    groundCheck = transform;
        //if (wallCheck == null)
        //    wallCheck = transform;

    }

    //private void FindPlayer()
    //{
    //    if (player != null)
    //        return;

    //    if (PlayerCrl.instance.transform != null)
    //        player = PlayerCrl.instance.transform;
    //}

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

    public virtual void Damage()
    {
        if (!invicinble)
        {
            canMove = false;
            anim.SetTrigger("gotHit");
        }
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
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
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
