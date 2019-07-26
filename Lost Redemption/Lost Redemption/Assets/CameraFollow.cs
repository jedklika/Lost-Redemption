using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public float smoothSpeed = 5f;

    public Vector3 offset;


    private void FixedUpdate()
    {
        Vector3 desiredPos = Target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}
