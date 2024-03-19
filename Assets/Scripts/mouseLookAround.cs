using UnityEngine;

public class mouseLookAround : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensitivity of mouse movement
    public Transform playerBody; // Reference to the player body to rotate horizontally

    private float xRotation = 0f; // For vertical rotation

    void Start()
    {
        
    }

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculating and clamping vertical rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //horizontal rotation to the player body
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
