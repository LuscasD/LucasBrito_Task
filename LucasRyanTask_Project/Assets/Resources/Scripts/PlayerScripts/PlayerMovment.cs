using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public Animator animator;

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

        bool isMoving = inputDirection.magnitude >= 0.1f;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            // Calculate rotation relative to camera
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

}
