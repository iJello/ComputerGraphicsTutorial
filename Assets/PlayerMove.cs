using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerMove : MonoBehaviour
{

    //movement
    public float moveSpeed;

    public Transform orientation;
    //Groundedstuff
    public float playerHeight;
    public LayerMask whatGround;
    bool grounded;
    public float groundDrag;

    //JumpStuff
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readytojump;



    Vector3 moveDirection;
    Vector2 moveInput;

    Rigidbody rb;

    public InputActionAsset PlayerControls;
    private InputAction moveAction;
    private InputAction jumpAction;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        getInputs();
    }

    
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        groundCheck();
        handleDrag();
        
        if (jumpAction.triggered && grounded)
        {
            jump();
            print("Jump");
        }
        verticalSpeedLimit();
    }
    private void FixedUpdate()
    {
        movePlayer();
        
        
    }
    private void getInputs()
    {
        moveAction = PlayerControls.FindActionMap("Gameplay").FindAction("Movement");
        jumpAction = PlayerControls.FindActionMap("Gameplay").FindAction("Jump");
        
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void movePlayer()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        Debug.Log(moveDirection);
        if (grounded)
        {
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {

            rb.AddForce(moveDirection * moveSpeed * 0.5f, ForceMode.Force);
        }
        
        
    }

    private void groundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatGround);
    }
    private void handleDrag()
    {
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }
        
    private void verticalSpeedLimit()
    {
        if (rb.linearVelocity.y > 50)
        {
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 50, rb.linearVelocity.z);
        }
        else if(rb.linearVelocity.y < -50)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -50, rb.linearVelocity.z);
        }

    }
    private void jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}