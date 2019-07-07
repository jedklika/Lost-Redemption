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
    public float losDistance;

    void Start()
    {
        animator = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, losDistance);
        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if (hitInfo.collider.CompareTag("Player"))
            {
                animator.SetBool("isAttacking", true);
            }
            StartCoroutine(Confused());
            Debug.Log("Started");
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * losDistance, Color.green);
            animator.SetBool("isAttacking", false);
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
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

    IEnumerator Confused()
    {
        speed = 0;
        yield return new WaitForSeconds(1f);
        Invoke("resetSpeed", 1.5f);
        animator.SetBool("isAttacking", false);
        Flip();
    }

    private void resetSpeed()
    {
        speed = 2;
    }

    private void Flip()
    {
        if (moveRight == true)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            moveRight = true;
        }
    }

    

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
