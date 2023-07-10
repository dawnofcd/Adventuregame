using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
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
    bool Isfascingright = true;
    private float jumpforce = 10f;
    public Transform Jumpaccept;
    public LayerMask ground;
    bool doublejump;

    [Header("walljump")]
    public bool isWallsilide = true;
    public bool Iswall;
    public LayerMask Wall;
    public Transform Wallaccept;
    public int WallsideSpeed = 2;
    public bool isWalljumping;
    public float wallJumpingdirect;
    public float wallJumptime = 0.2f;
    public float wallJumpingcounter;
    public float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(2f, 8f);



    [Header("Animation")]
    public Animator Ani;
    string CurrenState;
    const string Runstate = "Run";
    const string Jumptstate = "Jump";
    const string Doublejumpstate = "Double_jump";
    const string Idlestate = "Idle";
    const string wallJumpstate = "Wall_jump";
    protected virtual void Changestate(string NewState)
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
    private void Update()
    {
        Checkground();
        Checkwall();
        MoveHorizal();
        Jumpping();
        Walljump();
        Slide();
    }

    public virtual void MoveHorizal()
    {
        rb.velocity = new Vector2(PlayerCrl.instance.inputManager.MoveX * speed, rb.velocity.y);
        if (PlayerCrl.instance.inputManager.MoveX != 0)
        {
            if (Isground) Changestate(Runstate);
            else
            {
                if (!doublejump && !Iswall ) Changestate(Doublejumpstate);
                else if (!Iswall) Changestate(Jumptstate);
                else if (Iswall) { Changestate(wallJumpstate); }
            }
        }
        else
        {
            if (Isground) Changestate(Idlestate);
            else
            {
                if (!doublejump) Changestate(Doublejumpstate);
                else
                {
                    if (!Iswall) Changestate(Jumptstate);
                    else Changestate(wallJumpstate);

                }
            }
        }
        if (!isWalljumping)
            Flip();
    }
    public virtual void Flip()
    {

        if (PlayerCrl.instance.inputManager.MoveX < 0 && !Isfascingright)
        {
            transform.localScale = new Vector3(-3, 3, 3);
            // spriteRenderer.flipX = true;
            Isfascingright = true;
        }
        if (PlayerCrl.instance.inputManager.MoveX > 0 && Isfascingright)
        {
            transform.localScale = new Vector3(3, 3, 3);
            //spriteRenderer.flipX = false;
            Isfascingright = false;
        }
    }

    void Checkground()
    {
        Isground = Physics2D.OverlapCircle(Jumpaccept.position, 0.1f, ground);
    }
    void Checkwall()
    {

        Iswall = Physics2D.OverlapCircle(Wallaccept.position, 0.2f, Wall);
    }
    void Jumpping()
    {

        if (PlayerCrl.instance.inputManager.Jumpkeydown)
        {
            if (Isground)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                doublejump = true;
            }
            else if (doublejump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                doublejump = false;
            }
        }
    }

    void Slide()
    {
        if (Iswall && !Isground && PlayerCrl.instance.inputManager.MoveX != 0f)
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
