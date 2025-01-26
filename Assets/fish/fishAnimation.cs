using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float moveSpeed = 2f;         // Speed of the fish moving forward
    public float rotationSpeed = 10f;    // Speed of the fish rotation (oscillation frequency)
    public float rotationAngle = 15f;   // Max angle the fish will rotate back and forth
    public float turnaroundTime = 10f;  // Time in seconds before turning around

    private float rotationTime = 0f;    // Timer to handle back-and-forth rotation
    private Quaternion initialRotation; // Store the initial rotation of the fish
    private float timer = 0f;           // Timer to track when to turn around
    private bool isReversed = false;    // Tracks if the fish is moving in the reversed direction

    void Start()
    {
        // Save the initial rotation at the start of the game
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to turn around
        if (timer >= turnaroundTime)
        {
            isReversed = !isReversed; // Reverse the direction
            timer = 0f;               // Reset the timer
        }

        // Move the fish forward or backward based on the current direction
        float direction = isReversed ? -1f : 1f;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Handle back-and-forth rotation relative to the initial direction
        rotationTime += Time.deltaTime * rotationSpeed;
        float angle = Mathf.Sin(rotationTime) * rotationAngle;
        transform.rotation =  Quaternion.Euler(direction * initialRotation.eulerAngles) * Quaternion.Euler(0, angle, 0); // Adjust axis if needed
    }
}
