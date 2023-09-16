using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("DataPlayer")]
    private int MaxHp=100;
    public int CurrentHp=100;
    public int CurrenHeath=2;
    private int Maxheath=3;
    private int Coin=0;


    private Rigidbody2D rb;
    private Animator anim;

    public int fruits;
    public int coins;

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public Vector2 wallJumpDirection;

    private float defaultJumpForce;

    private bool canDoubleJump = true;
    private bool canMove;

    private float movingInput;

    [SerializeField] private float bufferJumpTime; //player trước khi chạm đất thì khi ấn nhảy sẽ nhảy (Nhảy đệm)
    private float bufferJumpCounter;
    [SerializeField] private float cayoteJumpTime;
    private float cayoteJumpCounter; //cho phép nhảy ở rìa mép Terrain (Nhảy trên không)                    
    private bool canHaveCayoteJump;

    [Header("Knockedback info")]
    [SerializeField] private Vector2 knockbackDirection;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float knockbackProtectionTime;

    private bool isKnocked;
    private bool canBeKnocked = true;


    [Header("Collision info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    private bool isGrounded;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;


    private bool facingRight = true;
    private int facingDirection = 1;

    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultJumpForce = jumpForce;
        anim.SetBool("Start",true);
    }

    public void TakeDame(int Damemager)
    {
        CurrentHp  -=Damemager;
    }


    void Update()
    {
       Hpbar.instance.UpdateHpbar(CurrentHp,MaxHp);
       Playerreceivedam.instance.CaculatorHp(ref CurrenHeath,Maxheath,ref CurrentHp,MaxHp);
        AnimationControllers();

        if (isKnocked)
           {anim.SetBool("isKnockeds",true);} 
           else{ anim.SetBool("isKnockeds",false);}

        FlipController();
        CollisionChecks();
        InputChecks();

        CheckForEnemy();

        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if (isGrounded)
        {    
            anim.SetBool("Start",false);
            canDoubleJump = true;
            canMove = true;

            if (bufferJumpCounter > 0)
            {
                bufferJumpCounter = -1;
                Jump();
            }

            canHaveCayoteJump = true;
        }

        else
        {
            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpCounter = cayoteJumpTime;
            }

        }

        if (canWallSlide)
        {
            isWallSliding = true;
            anim.SetBool("isWallSliding", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
        }
        Move();
    }



 
    private void CheckForEnemy() //enemy
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                Enemy newEnemy = enemy.GetComponent<Enemy>();
                 
                  
                if (newEnemy.invicinble)
                    return;

                if (rb.velocity.y < 0)
                {

                    newEnemy.Damage();
                    Jump();  
                }
              
            }
        }
    }

    private void StopFlippingAnimation()
    {
        anim.SetBool("flipping", false);
    }

    // private void StopWallSlidingAnimation()
    // {
    //     anim.SetBool("isWallSliding", false);
    // }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isWallDetected", isWallDetected);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }


    private void InputChecks()
    {
        movingInput = Input.GetAxisRaw("Horizontal");
        //nhảy trên tường
        if (Input.GetAxis("Vertical") < 0)
            canWallSlide = false;

        //nhảy trên layer Ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
            anim.SetBool("isWallSliding", false);
        }
    }

    private void JumpButton()//double jump
    {
        if (!isGrounded)
        {
            bufferJumpCounter = bufferJumpTime;

        }

        if (isWallSliding) //wall jump
        {
            WallJump();
            canDoubleJump = true;
        }

        else if (isGrounded || cayoteJumpCounter > 0)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            anim.SetBool("flipping", true);
            Jump();
            jumpForce = defaultJumpForce;
        }

        canWallSlide = false;

    }

    public void KnockBack(Transform damageTransform) //bị dính dame
    {
        if (!canBeKnocked) 
        return;
        // fruits --; // hết food thì chết
        // if(fruits <0)
        // {
        //     Destroy(gameObject);
        // }

        //GetComponent<CameraShakeFx>().ScreenShake(-facingDirection);
         isKnocked = true; 
         
        canBeKnocked = false;

        #region Define horizontal direction for knockback
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x)
            hDirection = 1;
        else if (transform.position.x < damageTransform.position.x)
            hDirection = -1;

        #endregion    
        
        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);

        Invoke("CancelKnockback", knockbackTime);
         
        Invoke("AllowKnockback", knockbackProtectionTime);
        
        
    }

    private void CancelKnockback()
    {
        isKnocked = false;
        
        
    }
    private void AllowKnockback()
    {
        canBeKnocked = true;
      
    }

    private void Move()//di chuyen
    {
        if (canMove)
            rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
    }

    private void WallJump() //nhảy tường
    {
        canMove = false;
        // anim.SetBool("isWallSliding", false);

        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }


    private void Jump()//nhay
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Push(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private void FlipController() //xoay người
    {
        if (facingRight && rb.velocity.x < -.1f)
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > .1f)
        {
            Flip();
        }

    }

    private void Flip() // xoay người
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    private void CollisionChecks()//nhay tren layer Ground, nhay tren wall
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);//ground
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);//wall

        if (isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
            // canDoubleJump = false;
        }
        if (!isWallDetected)
       
         {   
        
            isWallSliding = false;
            canWallSlide = false;
        }

    }
public void Die()
{
     anim.SetBool("IsDie",true);
    
}
    private void OnDrawGizmos()//Xét khoảng cách từ player xuống Ground, Xet khoảng cách từ player đến wall
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance)); //ground
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y)); // wall
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius); // enemy


    }
}


