using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class Movemanager : MonoBehaviour
{
    [Header("Move")]
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
   protected virtual void Changestate( string NewState)
    {
        if (CurrenState == NewState) return;
        Ani.Play(NewState);
        CurrenState = NewState;
    }
    private void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
         Ani= GetComponent<Animator>();
         spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        MoveHorizal();
        Jumpping();
        
          
    }

    public virtual void MoveHorizal()
    {
       
        if (PlayerCrl.instance.inputManager.MoveX != 0)
        {
            rb.velocity = new Vector2(PlayerCrl.instance.inputManager.MoveX*speed, rb.velocity.y);
            Changestate(Runstate);
        }
        else Changestate(Idlestate);
        Filip();    
    }
    public virtual void Filip()
    {
        if (PlayerCrl.instance.inputManager.MoveX< 0 && !Isfascingright)
        { spriteRenderer.flipX = true; Isfascingright = true; }
        if (PlayerCrl.instance.inputManager.MoveX > 0 && Isfascingright)
        {spriteRenderer.flipX = false; Isfascingright = false; }
    }

    
    
    void Jumpping()
    {
        Isground = Physics2D.OverlapCircle(Jumpaccept.position, 0.1f, ground);

        if (Isground &&!PlayerCrl.instance.inputManager.Jumkey)
        { doublejump = false; }
        if (PlayerCrl.instance.inputManager.Jumpkeydown)
        {
            Changestate(Jumptstate);
            if (Isground||doublejump)
            { 
                
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                Changestate(Doublejumpstate);
                doublejump = !doublejump;
            }
        }
       
    } 
}
    