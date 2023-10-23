using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : Danger
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject pieces1Prefab;
    [SerializeField] private GameObject pieces2Prefab;
    [SerializeField] private Transform pieces1Origin;
    [SerializeField] private Transform pieces2Origin;

    private float xPeed;
    private float ySpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        rb.velocity = new Vector2(xPeed, ySpeed);

    }

    public void SetupSpeed(float x, float y)
    {
        xPeed = x;
        ySpeed = y;

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        base.OnTriggerEnter2D(collision);

        CreatePieces1();
        CreatePieces2();
        Destroy(gameObject);

    }

    private void CreatePieces1()
    {
        GameObject newPieces1 = Instantiate(pieces1Prefab,pieces1Origin.transform.position, pieces1Origin.transform.rotation );
        newPieces1.GetComponent<Bullet>();
        Destroy(newPieces1, 2.5f);        

    }
    private void CreatePieces2()
    {
        GameObject newPieces2 = Instantiate(pieces2Prefab,pieces2Origin.transform.position, pieces2Origin.transform.rotation );
        newPieces2.GetComponent<Bullet>();
        Destroy(newPieces2, 2.5f);

    }



}





