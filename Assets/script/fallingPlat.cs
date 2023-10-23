using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class fallingPlat : MonoBehaviour
{
    private float fallDelay = 0.1f;
    private float destroyDelay = 2f;
    [SerializeField] private Rigidbody2D rb;
    private float defaultGravityScale;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.GetComponent<Player>() != null)
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        defaultGravityScale= rb.gravityScale;
        rb.gravityScale = 2;
        Destroy(gameObject, destroyDelay);
    }
}
