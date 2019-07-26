using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTimer : MonoBehaviour
{
    public float timer = 8;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
