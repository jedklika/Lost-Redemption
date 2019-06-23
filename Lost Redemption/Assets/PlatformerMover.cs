using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMover : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    Vector3 Nextpos;
    // Start is called before the first frame update
    void Start()
    {
        Nextpos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
        {
            Nextpos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            Nextpos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, Nextpos, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
