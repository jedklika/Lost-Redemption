using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float Speed = 10;
    public float jumpHeight = 10;
    private bool isJumping = false;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;


    [SerializeField]
    private int health;

    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) && timeBtwAttack <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Debug.Log("Enemy damage" + enemiesToDamage[i]);
                enemiesToDamage[i].GetComponent<BasicEnemy>().health -= damage;

            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rb.velocity = new Vector3(-Speed, rb.velocity.y, 0f);
            //transform.localScale = new Vector3(-4, 4, transform.localScale.z);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rb.velocity = new Vector3(Speed, rb.velocity.y, 0f);
            //transform.localScale = new Vector3(4, 4, transform.localScale.z);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumping = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (isJumping == true)
        {
            this.gameObject.transform.parent = null;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform") && isJumping)
        {
            isJumping = false;
            this.gameObject.transform.parent = col.gameObject.transform;
            if (isJumping == true)
            {
                this.gameObject.transform.parent = null;

            }
        }
        if (col.gameObject.CompareTag("Falling") && isJumping)
        {
            isJumping = false;

        }
        if (col.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
            Debug.Log("Check");
            this.gameObject.transform.parent = null;
            if (isJumping == true)
            {
                this.gameObject.transform.parent = null;
            }
        }
        if (col.gameObject.CompareTag("Danger"))
        {
            SceneManager.LoadScene(0);
        }
        if (col.gameObject.CompareTag("End"))
        {
            SceneManager.LoadScene(0);
        }

        if (!invincible)    //if NOT invincible
        {
            if (col.gameObject.CompareTag("Foe"))   //if player collides with Foe
            {
                health -= 4;
                invincible = true;
                Speed = 0;
                jumpHeight = 0;

                Invoke("resetMovement", .2f);
                Invoke("resetInvulnerability", 2);


            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))   //if player collides with Foe
        {
            health -= 2;
            invincible = true;
            Speed = 0;
            jumpHeight = 0;

            Invoke("resetMovement", .2f);
            Invoke("resetInvulnerability", 2);
        }
    }


    private void resetMovement()   //Ends Stun
    {
        Speed = 10;
        jumpHeight = 10;
    }

    private void resetInvulnerability()     //Ends invincibility frames
    {
        invincible = false;
    }
}
