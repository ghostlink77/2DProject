using UnityEngine;

public class Dino : MonoBehaviour
{
    Rigidbody2D rigid;
    float moveSpeed = 1.2f;
    int direction = 1;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rigid.position.x + direction * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        rigid.linearVelocityX = moveSpeed * direction;
    }
}
