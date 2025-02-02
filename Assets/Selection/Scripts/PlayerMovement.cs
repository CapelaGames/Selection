using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private float speed = 500;//Movement speed
    private float maxSpeed = 3;
    private float stoppingForce = 2;
    public float rotationSpeed = 10f; // Speed of rotation

    //Physics engine access
    private Rigidbody rb;
    private Vector3 input = Vector3.zero;
    private Camera playerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = FindFirstObjectByType<Camera>();

        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        //Forward and Right of camera so input is relative to camera
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0;
        cameraForward.Normalize();

        //Get input
        input = (Input.GetAxisRaw("Horizontal") * cameraRight) + (Input.GetAxisRaw("Vertical") * cameraForward);


        //Add speed
        input.Normalize();
        input *= speed;
    }

    //Physics related thing should be done in fixed update
    void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, input.sqrMagnitude))
        {
            rb.AddForce(input * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(-rb.linearVelocity * stoppingForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        RotateTowardsMovementDirection();
    }

    void RotateTowardsMovementDirection()
    {
        if (Mathf.Approximately(0f, input.sqrMagnitude))
        {
            return;
        }
        // Calculate the target rotation based on the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(input.normalized);

        // Smoothly rotate towards the target rotation
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
