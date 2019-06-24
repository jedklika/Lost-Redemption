using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("DropPlatform", 0.1f);
        }
        if (col.gameObject.CompareTag("Danger"))
        {
            Destroy(this.gameObject);
        }
    }
    void DropPlatform()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

}