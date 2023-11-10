using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTarget; // Drag your camera target GameObject here
    public float cameraSpeed = 5f; // Adjust the camera movement speed

    private void Update()
    {
        // Check for player input to change the camera target position
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveCameraToNewPosition(new Vector3(10f, 0f, -10f)); // Example new position
        }

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, Time.deltaTime * cameraSpeed);
    }

    private void MoveCameraToNewPosition(Vector3 newPosition)
    {
        cameraTarget.position = newPosition;
    }
}
