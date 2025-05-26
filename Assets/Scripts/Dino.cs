using UnityEngine;

public class Dino : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    float moveSpeed = 1.7f;
    int direction = 1;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        SetMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + direction * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform", "Enemy"));

        if (rayHit.collider == null)
        {
            Turn();
        }
        rigid.linearVelocityX = moveSpeed * direction;
    }

    void SetMovement()
    {
        direction = Random.Range(-1, 2);

        float duration = direction == 0 ? 0.5f: Random.Range(1.5f, 3.5f);
        Invoke("SetMovement", duration);

        animator.SetFloat("Speed", Mathf.Abs(direction));
        spriter.flipX = direction == 1;
    }

    void Turn()
    {
        direction *= -1;
        spriter.flipX = direction == 1;
        CancelInvoke();
        Invoke("SetMovement", Random.Range(1.5f, 3.5f));
    }
}
