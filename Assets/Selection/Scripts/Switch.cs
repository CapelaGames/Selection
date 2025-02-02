using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public UnityEvent activate;
    public UnityEvent deactivate;
    public bool canBoxActivate = false;
    private bool isPlayerNear = false;

    private float number = 0;

    void Update()
    {
        // Check if the player is near the switch
        if (canBoxActivate == false && isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            activate?.Invoke();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is near the switch
        if (other.GetComponent<PlayerMovement>() == true)
        {
            isPlayerNear = true;
        }

        if (canBoxActivate && other.GetComponent<PlayerMovement>() == false && other.GetComponent<Rigidbody>() == true)
        {
            number++;
            activate?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player has left the switch area
        if (other.GetComponent<PlayerMovement>() == true)
        {
            isPlayerNear = false;
            deactivate?.Invoke();
        }

        if (canBoxActivate && other.GetComponent<PlayerMovement>() == false && other.GetComponent<Rigidbody>() == true)
        {
            number--;
            deactivate?.Invoke();
        }

    }
}
