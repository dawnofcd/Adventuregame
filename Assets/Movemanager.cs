using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class Movemanager : MonoBehaviour
{
    private float speed = 5f;
    //float jumpforce=5f;
    Rigidbody2D rb;
    Vector2 Vector2;
    public Animator Ani;
    public bool checkground;
    bool Isfascingright = true;
    private float jumpforce = 5f;
    public Transform Jumpaccept;
    public LayerMask ground;
    bool doublejump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //  Ani= GetComponent<Animator>();
    }
    private void Update()
    {
        MoveHorizal();
        Filip();
        Anima();
        Jumpping();
    }

    public virtual void MoveHorizal()
    {
        rb.velocity = new Vector2(inputManager.Instance.Move * speed, rb.velocity.y);
        if (inputManager.Instance.Numberjump <= 2 && checkground)
        { rb.AddForce(Vector2.up * inputManager.Instance.MoveJump, ForceMode2D.Impulse); }

    }
    public virtual void Filip()
    {
        if (inputManager.Instance.Move < 0 && !Isfascingright && checkground)
        { this.transform.localScale = new Vector3(-1, 1, 1); Isfascingright = true; }
        if (inputManager.Instance.Move > 0 && Isfascingright)
        { transform.localScale = new Vector3(1, 1, 1); Isfascingright = false; }
    }

    void Anima()
    {
        Ani.SetFloat("run", Mathf.Abs(inputManager.Instance.Move));
    }

    void Jumpping()
    {
        checkground = Physics2D.OverlapCircle(Jumpaccept.position, 0.1f, ground);
        if (checkground && !Input.GetKey(KeyCode.Space))
        {
            doublejump = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            if (checkground || doublejump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                doublejump = !doublejump;
            }
        } 
    } 
}
    