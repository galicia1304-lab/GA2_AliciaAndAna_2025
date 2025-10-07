using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Attributes")]
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent = 1;

    [Header("Smoothen's The Turn Rotation")]
    [Range(0, 0.2f)]
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    [Header("Extra Control (Usually Fine at 0 Though)")]
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    public float currentSpeed;
    float velocityY;

    Transform cameraT;
    CharacterController controller;

    [Header("Character Movement Check")]
    public bool isMoving;
    public bool isMovingLateral;

    [Header("Camera Setting")]
    public bool bUseCameraControlRotation; // capsule follows camera if true

    [Header("PlayerAnimator")]
    public PlayerAnimator playerAnimator;

    // ADDED: Footstep fields
    [Header("Footstep Settings")]
    public AudioSource footstepSource;        
    public AudioClip[] footstepClips;         
    public float walkStepInterval = 0.5f;     
    public float runStepInterval = 0.3f;      
    public float sneakStepInterval = 0.8f;    
    float footstepTimer = 0f;                 

    void Start()
    {
        cameraT = Camera.main.transform;             // Camera initial transform cache
        controller = GetComponent<CharacterController>(); // Cache CharacterController
    }

    void Update()
    {
        // input detection
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);
        bool sneaking = Input.GetKey(KeyCode.LeftControl);

        // Movement
        Move(inputDir, running, sneaking);

        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        // Animation
        Animate(input, running, sneaking);

        // ADDED: Footsteps (after movement/animation so currentSpeed & grounded are up to date)
        HandleFootsteps(running, sneaking);
    }

    void Animate(Vector2 _input, bool _run, bool _sneak)
    {
        if (playerAnimator == null) { return; }

        if (_input.magnitude > 0)
        {
            if (_run) { playerAnimator.Run(); }
            else if (_sneak) { playerAnimator.Sneak(); }
            else { playerAnimator.Walk(); }
        }
        else
        {
            playerAnimator.Idle();
        }
    }

    void Move(Vector2 inputDir, bool running, bool sneaking)
    {
        // Rotation
        if (!bUseCameraControlRotation || bUseCameraControlRotation)
        {
            if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    targetRotation,
                    ref turnSmoothVelocity,
                    GetModifiedSmoothTime(turnSmoothTime)
                );
            }
        }

        // Speed selection
        float targetSpeed = walkSpeed * inputDir.magnitude; // default
        if (running) { targetSpeed = runSpeed * inputDir.magnitude; }
        if (sneaking) { targetSpeed = 0.25f * walkSpeed * inputDir.magnitude; }

        // Smooth to target speed
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        // Gravity
        if (velocityY > -5) { velocityY += Time.deltaTime * gravity; }

        // Compose velocity
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        // Move
        controller.Move(velocity * Time.deltaTime);

        // Recompute planar speed from controller
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        // Movement flags
        isMoving = currentSpeed != 0;

        Vector3 velolat = transform.forward * currentSpeed;
        isMovingLateral = velolat.magnitude > 0;
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded) { return smoothTime; }
        if (airControlPercent == 0) { return float.MaxValue; }
        return smoothTime / airControlPercent;
    }

    public void Teleport(Transform destination)
    {
        // place the player
        controller.Move(destination.position - transform.position);
        transform.SetPositionAndRotation(destination.position, destination.rotation);
        cameraT.SetPositionAndRotation(destination.position, destination.rotation);

        Debug.Log("dest " + destination.name);
    }

    // ADDED: Footstep logic
    void HandleFootsteps(bool running, bool sneaking)
    {
        // Must have audio setup and be moving on the ground
        if (footstepSource == null || footstepClips == null || footstepClips.Length == 0) { return; }
        if (!controller.isGrounded || !isMoving) { footstepTimer = 0f; return; }

        // Choose interval based on state
        float interval = walkStepInterval;
        if (running) interval = runStepInterval;
        if (sneaking) interval = sneakStepInterval;

        // Scale by speed so diagonal / faster movement feels right
        // (Optional but nice: shorter interval when moving faster than base walk)
        float speedFactor = Mathf.Max(0.25f, currentSpeed / Mathf.Max(0.01f, walkSpeed));
        float effectiveInterval = interval / speedFactor;

        footstepTimer += Time.deltaTime;
        if (footstepTimer >= effectiveInterval)
        {
            PlayFootstep();
            footstepTimer = 0f;
        }
    }

    // ADDED: Single-step playback with subtle variation
    void PlayFootstep()
    {
        int index = Random.Range(0, footstepClips.Length);
        // Small variation for realism
        footstepSource.pitch = Random.Range(0.95f, 1.05f);
        footstepSource.volume = Random.Range(0.9f, 1f);
        footstepSource.PlayOneShot(footstepClips[index]);
    }
}
