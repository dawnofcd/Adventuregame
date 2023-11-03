
using UnityEngine;


public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public Vector2 wallJumpDirection;

    private float defaultJumpForce;

    private bool canDoubleJump = true;
    private bool canMove;

    [SerializeField] private float movingInput;

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
    [SerializeField] public static bool isGrounded;
    public static bool isWallDetected;
    private bool canWallSlide;
    public static bool isWallSliding;
    private bool facingRight = true;
    private int facingDirection = 1;

    //private bool canBeControlled;
    [SerializeField] bool testingOnPC;
    public VariableJoystick joystick;
    float hInput;
    float vInput;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultJumpForce = jumpForce;

        for (int i = 0; i < anim.layerCount; i++)
        {
            if (i == PlayerManager.choosenSkinId)
            {
                anim.SetLayerWeight(i, 1f);
            }
        }

    }




    void Update()
    {

        AnimationControllers();

        if (isKnocked)
        {
            return;
        }


        FlipController();
        CollisionChecks();
        InputChecks();
        CheckForEnemy();
        CheckForBox();

        bufferJumpCounter -= Time.deltaTime;
        cayoteJumpCounter -= Time.deltaTime;

        if (isGrounded)
        {

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
                    Audiomanager.instance.PlayKicked();
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
        anim.SetBool("isWallSliding", isWallSliding);
        // anim.SetBool("canBeControled", canBeControlled);
    }


    private void InputChecks()
    {
        // if(!canBeControlled)
        //     return;

        if (testingOnPC)
        {
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");

        }
        else
        {
            hInput = joystick.Horizontal;
            vInput = joystick.Vertical;
        }

        //nhảy trên tường
        if (vInput < 0)
            canWallSlide = false;

        //nhảy trên layer Ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
            anim.SetBool("isWallSliding", false);
        }
    }

    public void JumpButton()//double jump
    {
        Audiomanager.instance.PlayJumpSound();
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

        // GetComponent<CameraShakeFx>().ScreenShake(-facingDirection);


        PlayerManager.instance.ScreenShake(-facingDirection);
        Audiomanager.instance.PlayKnocked();
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
        {
            rb.velocity = new Vector2(moveSpeed * hInput, rb.velocity.y);
        }
    }

    public void ReturnControll()
    {
        //  rb.gravityScale = defaultGravityScale;
        // canBeControlled =  true;
    }

    private void WallJump() //nhảy tường
    {
        canMove = true;
        anim.SetBool("isWallSliding", false);
        Audiomanager.instance.PlayWallsilde();
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }


    private void Jump()//nhay
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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


    private void CheckForBox()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        foreach (var box in hitedColliders)
        {
            if (box.GetComponent<Box>() != null)
            {
                Box newEnemy = box.GetComponent<Box>();

                if (newEnemy.invicinble)
                    return;


                if (rb.velocity.y < 0 || isWallDetected)
                {
                    Audiomanager.instance.PlayKicked();

                    newEnemy.Damage();
                    Jump();
                }

            }
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
    private void OnDrawGizmos()//Xét khoảng cách từ player xuống Ground, Xet khoảng cách từ player đến wall
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance)); //ground
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y)); // wall
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius); // enemy


    }
}


