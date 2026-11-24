using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public Vector2 movement;
    //public bool grounded;
    public float jumpHeight = 5;

    public Vector2 velocity;
    public float acceleration;
    public float deceleration;

    public float apexHeight = 3f;
    public float apexTime = 0.5f;

    public float gravity;
    public float jumpVelocity;
    public bool isJumping;

    public enum FacingDirection
    {
        left, right, idle
    }

    void Start()
    {
        gravity  = -2 * apexHeight / Mathf.Pow(apexHeight, 2);
        jumpVelocity = 2 * apexHeight / apexTime;
    }

    void Update()
    {
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);

        // grounded = IsGrounded();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            this.GetComponent<Rigidbody2D>().AddForceY(jumpHeight);
            //isJumping = true;
        }
    }

    public void MovementUpdate(Vector2 playerInput)
    {
        transform.position += (Vector3) movement * speed * Time.deltaTime;
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //if (!IsGrounded())
        //{
        //    movement.y += gravity * Time.deltaTime;
        //}
        //else
        //{
        //    movement.y = 0;
        //}

        if (isJumping)
        {
            isJumping = false;
            movement.y = jumpVelocity;
        }
    }

    public bool IsWalking()
    {
        if (movement.x != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsGrounded()
    {
        //bool hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1, Color.red);

        return Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Ground"));
    }

    public FacingDirection GetFacingDirection()
    {
        if (movement.x >= 0.1)
        {
            return FacingDirection.right;
        }
        else if (movement.x <= -0.1)
        {
            return FacingDirection.left;
        }
        else
        {
            return FacingDirection.idle;
        }
    }
}
