using UnityEngine;

public class Player : MonoBehaviour
{
    public int Hp = 3;
    public bool indamaged = false;

    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.transform.position);
        }
    }

    public void TakeDamage(Vector2 targetPos)
    {
        if (indamaged) return;
        indamaged = true;
        Hp--;
        if (Hp <= 0)
        {
            Debug.Log("Player is dead");
        }
        else
        {
            Debug.Log($"You took damage. current HP : {Hp}");
        }
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc * 9, 3), ForceMode2D.Impulse);
        Invoke("OutDamage", 0.2f);
    }
    void OutDamage()
    {
        indamaged = false;
    }
}
