using UnityEngine;

// This script requires a CharacterController component to be attached to the same GameObject.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [Tooltip("The speed at which the player moves.")]
    public float moveSpeed = 5.0f;

    [Header("Camera Look")]
    [Tooltip("The camera that will follow the player's point of view.")]
    public Camera playerCamera;
    [Tooltip("The sensitivity of the mouse look.")]
    public float mouseSensitivity = 2.0f;
    [Tooltip("The maximum angle the camera can look up and down.")]
    public float verticalLookLimit = 80.0f;

    // Private variables
    private CharacterController characterController;
    private float verticalRotation = 0f; // Stores the current vertical rotation of the camera

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();

        // Lock the cursor to the center of the screen and make it invisible
        // This is standard for first-person games.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Player Movement (WASD) ---

        // Get input from the horizontal (A/D or Left/Right Arrow) and vertical (W/S or Up/Down Arrow) axes.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement direction vector based on the player's forward and right directions.
        // This makes the movement relative to where the player is looking.
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Apply the movement to the CharacterController.
        // We multiply by moveSpeed to control the speed and Time.deltaTime to make it frame-rate independent.
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);


        // --- Mouse Look (Camera Control) ---

        // Get mouse movement input.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the entire player GameObject left and right based on horizontal mouse movement.
        transform.Rotate(Vector3.up * mouseX);

        // Calculate the vertical rotation for the camera.
        // We subtract mouseY because a positive Y mouse movement should mean looking up (negative rotation around X-axis).
        verticalRotation -= mouseY;

        // Clamp the vertical rotation to prevent the player from looking all the way up and over.
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        // Apply the vertical rotation to the camera only.
        // This prevents the entire player capsule from tilting up and down.
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
