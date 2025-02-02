using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public LayerMask raycastLayers;

    public float pickUpDistance = 3f; // Maximum distance to pick up an object
    public float holdDistance = 2f; // Distance in front of the player to hold the object
    public float throwForce = 10f; // Force to throw the object

    private Rigidbody heldObject = null; // Reference to the currently held object
    private Vector3 holdPosition; // Position to hold the object

    void Update()
    {
        // Check for input to pick up or release an object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                ReleaseObject();
            }
        }

        // If holding an object, update its position
        if (heldObject != null)
        {
            HoldObject();
        }
    }

    void TryPickUpObject()
    {
        // Raycast to detect objects in front of the player
        Ray ray = new Ray(transform.position + (Vector3.up * 0.5f), transform.forward); // Ray starts from player and goes forward
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpDistance, raycastLayers))
        {
            if (hit.transform == transform) return;

            // Check if the hit object has a Rigidbody
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Pick up the object
                heldObject = rb;
                heldObject.useGravity = false; // Disable gravity while holding
                heldObject.freezeRotation = true; // Freeze rotation while holding
            }
        }
    }

    void HoldObject()
    {
        //if the object is behind you, release
        if (Vector3.Dot((heldObject.position - transform.position).normalized, transform.forward) < 0.25f)
        {
            ReleaseObject();
            return;
        }

        // Calculate the hold position in front of the player
        holdPosition = transform.position + transform.forward * holdDistance;

        // Smoothly move the object to the hold position
        heldObject.linearVelocity = (holdPosition - heldObject.position) * 10f;

        Vector3 wiggle = heldObject.rotation.eulerAngles;
        wiggle.x += Mathf.Sin(Time.time * 10f) * 0.1f;
        wiggle.z += Mathf.Cos(Time.time * 10f) * 0.1f;
        heldObject.rotation = Quaternion.Euler(wiggle);

        // Check for input to throw the object
        if (Input.GetKeyDown(KeyCode.Space)) // Left mouse button
        {
            ThrowObject();
        }
    }

    void ReleaseObject()
    {
        heldObject.freezeRotation = false; // Unfreeze rotation

        //Add a rotation and upwards force
        heldObject.linearVelocity = Vector3.zero;
        heldObject.AddForce(Vector3.up * 3f, ForceMode.VelocityChange);
        Vector3 randomAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        heldObject.AddTorque(randomAxis * 400f);

        // Release the object
        heldObject.useGravity = true; // Re-enable gravity
        heldObject = null; // Clear the reference

    }

    void ThrowObject()
    {
        heldObject.freezeRotation = false;
        Vector3 randomAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        heldObject.AddTorque(randomAxis * 400f);

        // Apply a force to throw the object in the direction the player is facing
        heldObject.useGravity = true;
        heldObject.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        heldObject = null; // Clear the reference
    }
}
