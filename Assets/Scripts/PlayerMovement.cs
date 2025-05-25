using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Sprite[] jumpSprites;
    
    float horizontalInput;
    bool isGrounded = true;
    bool isJumping = false;
    //bool isWall = false;
    bool isKnockingBack = false;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriter;

    [SerializeField] GameObject playerAttackRange;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
        //Debug.Log(isKnockingBack);

    }

    void FixedUpdate()
    {
        //if (player.indamaged) return;
        if (!isKnockingBack)
        {
            Move();
        }
        if(isJumping)
        {
            Jump();
        }
        rigid.linearVelocityY = Mathf.Clamp(rigid.linearVelocityY, -20f, 20f);
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

    public void Jump()
    {
        isJumping = false;
        Debug.Log("Jump");
        rigid.AddForceY(jumpForce, ForceMode2D.Impulse);
    }
    public void Knockback(int dirc)
    {
        isKnockingBack = true;
        rigid.AddForce(new Vector2(dirc * 9, 3), ForceMode2D.Impulse);
        Invoke("OutKnockback", 0.2f);
    }
    void OutKnockback()
    {
        isKnockingBack = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = collision.contacts[0].normal.y > 0.3f;
            playerAttackRange.SetActive(false);
            //isWall = Mathf.Abs(collision.contacts[0].normal.x) > 0.5f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
            //isWall = false;
            playerAttackRange.SetActive(true);
        }
    }
}
