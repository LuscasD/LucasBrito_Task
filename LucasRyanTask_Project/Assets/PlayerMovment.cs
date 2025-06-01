using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;

    private CharacterController characterController;
    private float turnSmoothVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Get the target rotation from camera + input direction
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Rotate the player to face movement direction
            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            // Move in the direction based on camera
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

}
