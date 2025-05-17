using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 1f;
    float jumpForce = 5f;
    bool isGrounded = true;
    Rigidbody2D rigid;
    Animator animator;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput != 0 && !animator.GetBool("isChange") && !animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isChange", true);
            transform.localScale = new Vector3(horizontalInput * 3, 3, 3);
            Debug.Log("Running");
        }
        else if(horizontalInput != 0 && animator.GetBool("isChange") && animator.GetBool("isRunning"))
        {
            animator.SetBool("isChange", false);
        }
        else if (horizontalInput == 0 && animator.GetBool("isRunning"))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isChange", true);
        }
        else
        {
            animator.SetBool("isChange", false);
        }

    }

    void FixedUpdate()
    {
        Move();
        //Jump();
    }

    void Move()
    {
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, 0);
        rigid.linearVelocityX = movement.x * moveSpeed;

    }
}
