using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    [HideInInspector] public bool canMove = true;

    Rigidbody2D rb;
    Vector2 move;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    if (!canMove) return;

    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    transform.Translate(new Vector3(x, y, 0) * 5f * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move;
    }
}
