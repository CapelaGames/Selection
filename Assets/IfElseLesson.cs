using UnityEngine;

public class IfElseLesson : MonoBehaviour
{
    // Public variables visible in Unity Editor
    public float speed = 5f;
    public Color happyColor = Color.green;
    public Color sadColor = Color.blue;

    // Students can modify these values in the Inspector
    public KeyCode happyKey = KeyCode.Space;
    public KeyCode sadKey = KeyCode.R;

    void Update()
    {
        // Basic If Statement Example
        if (Input.GetKey(KeyCode.W))
        {
            // Move forward when W is pressed
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        /* STUDENT MODIFICATION AREA 1: 
        Add an else-if statement here to move backward when S is pressed
        Uncomment this section to practice
        
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        */

        // If/Else Color Change Example
        if (Input.GetKeyDown(happyKey))
        {
            GetComponent<Renderer>().material.color = happyColor;
            Debug.Log("I'm happy! :D");
        }
        else if (Input.GetKeyDown(sadKey))
        {
            GetComponent<Renderer>().material.color = sadColor;
            Debug.Log("I'm sad :(");
        }
        else
        {
            // This runs if neither key is pressed
            //Debug.Log("Press Space or R to see colors!");
        }

        /* STUDENT MODIFICATION AREA 2:
        Create your own if/else statement below!
        Try checking for different keys (KeyCode.T, KeyCode.Y, etc)
        Add new actions like rotation or scale changes
        */
    }
}
