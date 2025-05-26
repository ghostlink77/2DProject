using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Hp = 3;
    public bool indamaged = false;

    [SerializeField] GameManager gameManager;
    Rigidbody2D rigid;
    PlayerMovement playerMovement;
    Animator animator;
    void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            gameManager.SetCheckPoint(collision.transform.position);
            SpriteRenderer spriter = collision.GetComponent<SpriteRenderer>();
            spriter.color = Color.green;
        }
        if (collision.CompareTag("Finish"))
        {
            gameManager.GameClear();
        }
    }


    void TakeDamage(Vector2 targetPos)
    {
        if (indamaged) return;
        indamaged = true;
        Hp--;
        if (Hp <= 0)
        {
            Die();
            return;
        }
        else
        {
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            playerMovement.Knockback(dirc);
        }
        Invoke("OutDamage", 1f);
    }

    public void Die()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Dead");
        gameManager.PlayerDie();
    }

    public void KillEnemy()
    {
        rigid.linearVelocityY = Input.GetButton("Jump") ? 14.5f : 9f;
        gameManager.GetScore(100);
    }

    void OutDamage()
    {
        indamaged = false;
    }
}
