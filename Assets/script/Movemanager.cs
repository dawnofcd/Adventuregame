using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class Movemanager : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 10f;
    public Rigidbody2D rb;
    Vector2 Vector2;
    public bool Isground;
    bool Isfascingright;
    private float jumpforce = 10f;
    public Transform Jumpaccept;
    public LayerMask ground;
    public bool doublejump;
   

    [Header("walljump")]
    public bool isWallsilide;
    public bool Iswall;
    public LayerMask Wall;
    public Transform Wallaccept;
    public float WallsideSpeed = 5f;
    public bool  isWalljumping;
    public float wallJumpingdirect;
    public float wallJumptime = 0.2f;
    public float wallJumpingcounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(4f, 16f);
    bool Checkwalljump;
    bool Checkhitdame;
    float TrapforceX = 5f;

    [Header("Animation")]
    public Animator Ani;
    string CurrenState;
    public bool AnimateCheckjump;
    const string Runstate = "Run";
    const string Jumptstate = "Jump";
    const string Doublejumpstate = "Double_jump";
    const string Idlestate = "Idle";
    const string wallJumpstate = "Wall_jump";
    const string Trap_damestate="Trap_state";
    const string hitTrap = "Hitdame";

   


    public virtual void Changestate(string NewState)
    {
        if (CurrenState == NewState) return;
        Ani.Play(NewState);
        CurrenState = NewState;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Checkhitdame = true;
            Vector2 pushDirection = transform.position - collision.transform.position;
            pushDirection.Normalize();
            this.rb.AddForce(TrapforceX*pushDirection, ForceMode2D.Impulse);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Checkhitdame = false;
        }
    }
    private void Update()
    {
        Animate();
        Checkground();
        Checkwall();
        MoveHorizal();
        Jumpping();
        Walljump();
        Slide();
        if (!isWalljumping)
        {
            Flip();
        }
    }

   
    public virtual void Animate()
    {

        if (PlayerCrl.instance.inputManager.MoveX != 0)
        {  
            AnimateCheckjump = true; // Neu nhan nut horizal thi 
            if (Isground) Changestate(Runstate);
              
            else
            {
              
                if (!doublejump || isWalljumping || doublejump)
                     Changestate(Jumptstate);
            }
        }
        else
        {
            if (Isground) Changestate(Idlestate);

            else
            {
                if (isWallsilide) Changestate(wallJumpstate);
                if (isWalljumping) Changestate(Jumptstate);
                if (!doublejump && !isWallsilide) Changestate(Jumptstate);
                if (doublejump && !AnimateCheckjump && !Checkhitdame) { Changestate(Doublejumpstate); }
                else if (Checkhitdame) Changestate(hitTrap);// dieu kien khong cho anima cua double jump xay ra khi walljump
            }
        }

    }
    public virtual void MoveHorizal()
    {
        rb.velocity = new Vector2(PlayerCrl.instance.inputManager.MoveX * speed, rb.velocity.y);
        Flip();
     }
    public virtual void Flip()
    {

        if (PlayerCrl.instance.inputManager.MoveX < 0 && !Isfascingright)
        {
            transform.localScale = new Vector3(-4, 4, 4);
            // spriteRenderer.flipX = true;
            Isfascingright = true;
        }
        if (PlayerCrl.instance.inputManager.MoveX > 0 && Isfascingright)
        {
            transform.localScale = new Vector3(4, 4, 4);
            //spriteRenderer.flipX = false;
            Isfascingright = false;
        }
    }

    void Checkground()
    {
        Isground = Physics2D.OverlapCircle(Jumpaccept.position, 0.1f, ground);
        if (Isground) AnimateCheckjump = false; // neu cham dat thi doublejump=fasle;

    }
    void Checkwall()
    {
        Iswall = Physics2D.OverlapCircle(Wallaccept.position, 0.2f, Wall);
      if(Iswall)  AnimateCheckjump = true; //neu cham tuong thi doublejump=true;
    }
    void Jumpping()
    {
       
        if (PlayerCrl.instance.inputManager.Jumpkeydown)
        {
            if (Isground)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                doublejump = false;
                AnimateCheckjump = true; 
            }
            else if (!doublejump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                doublejump = true;
            }
        }
        Flip();
    }

    void Slide()
    {
        if (Iswall && !Isground && PlayerCrl.instance.inputManager.MoveX == 0f)
        {

            isWallsilide = true;
            rb.velocity = new Vector2(rb.velocity.x, math.clamp(rb.velocity.y, -WallsideSpeed, float.MaxValue));
        }
        else { isWallsilide = false; }
    }

    void Walljump()
    {
        if (isWallsilide)
        {
            isWalljumping = false;
            wallJumpingdirect = -transform.localScale.x;
            wallJumpingcounter = wallJumptime;
            CancelInvoke(nameof(Stopwalljumping));

        }
        else
        {
            wallJumpingcounter -= Time.deltaTime;
        }
        if (PlayerCrl.instance.inputManager.Jumpkeydown && wallJumpingcounter > 0f)
        {
            isWalljumping = true; rb.velocity = new Vector2(wallJumpingdirect * wallJumpingPower.x, wallJumpingPower.y);

            wallJumpingcounter = 0f;
            if (transform.localScale.x != wallJumpingdirect)
            {
                Isfascingright = !Isfascingright;
                Vector3 LocalScale = transform.localScale;
                LocalScale.x *= -1f;
                transform.localScale = LocalScale;

            }
        }
        Invoke("Stopwalljumping", wallJumpingDuration);
    }

    void Stopwalljumping()
    {
        isWalljumping = false;
    }

    
}
