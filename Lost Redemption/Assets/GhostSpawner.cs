using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("SpawnEnemy", .5f);
            Debug.Log("enemy has spawned");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("ResetCollider", 10f);
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(enemy, enemyPos.position, enemyPos.rotation);
    }

    private void ResetCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
