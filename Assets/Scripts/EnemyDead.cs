using UnityEngine;

public class EnemyDead : MonoBehaviour
{
    Animator animator;
    CapsuleCollider2D coll;
    Rigidbody2D rigid;
    private void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttackRange"))
        {
            coll.enabled = false;
            rigid.bodyType = RigidbodyType2D.Static;

            collision.GetComponentInParent<Player>().KillEnemy();
            Debug.Log("Enemy Dead");
            animator.SetTrigger("Dead");
            Invoke("DestroyEnemy", 0.3f);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
