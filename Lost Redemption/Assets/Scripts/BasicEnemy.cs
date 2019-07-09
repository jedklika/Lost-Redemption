using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed;
    public float Distance;
    public bool moveRight = true;
    public Transform groundDetection;
    public int health;

    public Animator animator;
    public bool canMove = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, Distance);
        if(groundInfo.collider == false)
        {
            if (moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            speed *= 2;
            animator.SetBool("isAttacking", true);
            Debug.Log("SPOTTED!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            speed /= 2;
            animator.SetBool("isAttacking", false);
            if (moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        canMove = false;
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
            Invoke("resetMobility", 1f);
            Debug.Log("Noted");
        }
    }

    private void resetMobility()
    {
        canMove = true;
    }
}
