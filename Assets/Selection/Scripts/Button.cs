using UnityEngine;

public class Button : MonoBehaviour
{
    float slideDistance = 0.05f; // Distance the door will slide down
    public float slideSpeed = 2f;   // Speed of the sliding motion

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isActivated = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.down * slideDistance;
    }

    void Update()
    {
        if (isActivated)
        {
            // Move the door downward
            transform.position = Vector3.MoveTowards(transform.position, endPosition, slideSpeed * Time.deltaTime);
        }
        else
        {
            // Move the door upward
            transform.position = Vector3.MoveTowards(transform.position, startPosition, slideSpeed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        isActivated = true;
    }

    public void Deactivate()
    {
        isActivated = false;
    }
}


