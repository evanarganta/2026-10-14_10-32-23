using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 0.15f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    public Transform playerCamera;

    private CharacterController controller;
    private float verticalVelocity;
    private float cameraPitch;
    private bool mouseLocked = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SetMouseLock(true);
    }

    void SetMouseLock(bool locked)
    {
        mouseLocked = locked;
        Cursor.lockState = locked
            ? CursorLockMode.Locked
            : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        // Right-click toggles mouse lock
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            SetMouseLock(!mouseLocked);
        }

        // Mouse look only while locked
        if (mouseLocked)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            transform.Rotate(
                Vector3.up * mouseDelta.x * mouseSensitivity
            );

            cameraPitch -= mouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

            if (playerCamera != null)
            {
                playerCamera.localRotation =
                    Quaternion.Euler(cameraPitch, 0f, 0f);
            }
        }

        // WASD movement
        float moveX = 0f;
        float moveZ = 0f;

        if (Keyboard.current.wKey.isPressed) moveZ += 1f;
        if (Keyboard.current.sKey.isPressed) moveZ -= 1f;
        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;

        Vector3 movement =
            (transform.right * moveX +
             transform.forward * moveZ).normalized;

        // Gravity and jumping
        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (controller.isGrounded &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            verticalVelocity =
                Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = movement * speed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        // Keep the player upright
        transform.rotation = Quaternion.Euler(
            0f, transform.eulerAngles.y, 0f
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddPoint();

            Destroy(other.gameObject);
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
