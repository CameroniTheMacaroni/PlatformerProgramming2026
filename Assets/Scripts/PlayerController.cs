using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public Vector2 movement;
    //public bool grounded;
    public float jumpHeight = 5;

    public enum FacingDirection
    {
        left, right, idle
    }

    void Start()
    {
        
    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);

        // grounded = IsGrounded();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            this.GetComponent<Rigidbody2D>().AddForceY(jumpHeight);
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
        Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);

        return Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Ground")); ;
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
