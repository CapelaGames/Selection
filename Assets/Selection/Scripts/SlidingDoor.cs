using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float slideDistance = 5f; // Distance the door will slide down
    public float slideSpeed = 2f;   // Speed of the sliding motion

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isSliding = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + Vector3.down * slideDistance;
    }

    void Update()
    {
        if (isSliding)
        {
            // Move the door downward
            transform.position = Vector3.MoveTowards(transform.position, endPosition, slideSpeed * Time.deltaTime);

            // Stop sliding when the door reaches the end position
            if (transform.position == endPosition)
            {
                isSliding = false;
            }
        }
    }

    // Call this method to trigger the door to slide
    public void TriggerSlide()
    {
        isSliding = true;
    }
}
