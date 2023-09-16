using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class camFolow : MonoBehaviour
{
    public Transform target;             // Reference to the player's transform
    public float smoothSpeed = 0.125f;    // Smoothness of camera movement
    public Vector3 offset;               // Offset between camera and player

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
