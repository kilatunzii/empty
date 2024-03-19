using UnityEngine;

public class liftSystem : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 1.0f;
    private bool goingUp = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Determine the next position based on the current direction of the lift
        Vector3 nextPosition = goingUp ? endPosition : startPosition;

        // Move the elevator towards the next position
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        // Check if the lift has reached its end position
        if (transform.position == nextPosition)
        {
            // switch direction for the next move
            goingUp = !goingUp;
        }
    }
}
