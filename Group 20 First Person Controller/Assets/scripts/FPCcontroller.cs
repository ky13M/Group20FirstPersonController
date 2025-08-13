using UnityEngine;
using UnityEngine.InputSystem;



    public class FPController : MonoBehaviour
    {
    public Camera playerCamera;
    public float normalFOV =60f;
    public float aimFOV = 40f;
    public float aimSpeed = 10f;

    private bool isAiming = false;

        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float gravity = -9.81f;
        public float jumpHeight = 1.5f;

        [Header("Look Settings")]
        public Transform cameraTransform;
        public float lookSensitivity = 2f;
        public float verticalLookLimit = 90f;
        private CharacterController controller;
        private Vector2 moveInput;
        private Vector2 lookInput;
        private Vector3 velocity;
        private float verticalRotation = 0f;
        

        [Header("Shooting")]
        public GameObject bulletPrefab;
        public Transform gunPoint;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;
    
    

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
        private void Update()
        {
            HandleMovement();
            HandleLook();

        if ((Input.GetMouseButtonDown(1)))
        {
            OnAim(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnAim(false);
        }
        float targetFOV = isAiming ? aimFOV : normalFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);
        }

    
    
    
     
    
    public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

    public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        }
    void OnAim(bool aiming)
    {
        isAiming = aiming;
    }
        public void OnShoot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Shoot();
            }
        }
        private void Shoot()
        {
            if (bulletPrefab != null && gunPoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(gunPoint.forward * 1000f); //Adjust force value as needed
                Destroy(bullet, 3);// Delete the bullet from the scene after 3 seconds
                }
                     
            }
        }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
         controller.height = crouchHeight;
          moveSpeed = originalMoveSpeed;
        }
    }
        public void HandleMovement()
        {
            Vector3 move = transform.right * moveInput.x + transform.forward *
            moveInput.y;
            controller.Move(moveSpeed * Time.deltaTime * move);
            if (controller.isGrounded && velocity.y < 0)
                velocity.y = -2f;
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        public void HandleLook()
        {
            float mouseX = lookInput.x * lookSensitivity;
            float mouseY = lookInput.y * lookSensitivity;
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
            verticalLookLimit);
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    public void Onshoot()
    {

    }
    }

