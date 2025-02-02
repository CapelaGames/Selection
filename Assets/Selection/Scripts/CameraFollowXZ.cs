using UnityEngine;

public class CameraFollowXZ : MonoBehaviour
{
    public Transform target; // The object to follow (e.g., the player)
    public Vector3 offset = new Vector3(0, 10, -10); // Camera offset from the target
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollowXZ: No target assigned!");
            return;
        }

        // Add the offset to the target position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current camera position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position (only X and Z are affected)
        transform.position = new Vector3(smoothedPosition.x, offset.y, smoothedPosition.z);
    }
}
