using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePrefab : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool Time = true;

    [SerializeField] Vector2 MinOffSet;
    [SerializeField] Vector2 MaxOffSet;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        Vector2 forcePieces = new Vector2(
            Random.Range(MinOffSet.x, MaxOffSet.x),
            Random.Range(MinOffSet.y, MaxOffSet.y)
        );
        if(Time)
        {
            rb.AddForce(forcePieces, ForceMode2D.Impulse);
            Time = false;
        }


    }












}
