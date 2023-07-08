using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class Movemanager : MonoBehaviour
{
    [Header("Movement")]
    private float speed = 10f;
    Rigidbody2D rb;
    Vector2 Vector2;
    public bool Isground;
    bool Isfascingright = true;
    private float jumpforce = 10f;
    public Transform Jumpaccept;
    public LayerMask ground;
    bool doublejump;
    SpriteRenderer spriteRenderer;

    [Header("Animation")]
    public Animator Ani;
    string CurrenState;
    const string Runstate = "Run";
    const string Jumptstate = "Jump";
    const string Doublejumpstate = "Double_jump";
    const string Idlestate = "Idle";
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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Checkground();
        MoveHorizal();
        Jumpping();
    }

    public virtual void MoveHorizal()
    {
        rb.velocity = new Vector2(PlayerCrl.instance.inputManager.MoveX * speed, rb.velocity.y);
        if (PlayerCrl.instance.inputManager.MoveX != 0)
        {
            if (Isground) Changestate(Runstate);
            else
            {

                if (!doublejump) Changestate(Doublejumpstate);

                else Changestate(Jumptstate);
            }
        }
        else
        {
            if (Isground) Changestate(Idlestate);
            else
            {
                if (!doublejump) Changestate(Doublejumpstate);
                else Changestate(Jumptstate);
            }
        }
        Flip();
    }
    public virtual void Flip()
    {
        if (PlayerCrl.instance.inputManager.MoveX < 0 && !Isfascingright)
        { spriteRenderer.flipX = true; Isfascingright = true; }
        if (PlayerCrl.instance.inputManager.MoveX > 0 && Isfascingright)
        { spriteRenderer.flipX = false; Isfascingright = false; }
    }

    void Checkground()
    {
        Isground = Physics2D.OverlapCircle(Jumpaccept.position, 0.1f, ground);
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
}
