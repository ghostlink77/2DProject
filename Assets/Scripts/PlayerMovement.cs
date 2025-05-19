using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Sprite[] jumpSprites;
    
    float horizontalInput;
    bool isGrounded = true;
    bool isJumping = false;

    Rigidbody2D rigid;
    Animator animator;
    Player player;
    SpriteRenderer spriter;

    [SerializeField] GameObject playerAttackRange;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

    }

    void FixedUpdate()
    {
        if (player.indamaged) return;
        Move();
        if(isJumping)
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        if(horizontalInput != 0)
            spriter.flipX = horizontalInput < 0;

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (!isGrounded)
        {
            if (rigid.linearVelocityY < 0)
            {
                spriter.sprite = jumpSprites[0];
            }
            else if (rigid.linearVelocityY > 0)
            {
                spriter.sprite = jumpSprites[1];
            }
        }
    }

    void Move()
    {
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, 0);
        rigid.linearVelocityX = movement.x * moveSpeed;

    }

    void Jump()
    {
        isJumping = false;
        Debug.Log("Jump");
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            playerAttackRange.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            playerAttackRange.SetActive(true);
        }
    }
}
