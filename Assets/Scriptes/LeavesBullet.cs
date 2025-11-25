using UnityEngine;

public class LeavesBullet : MonoBehaviour
{
    Vector2 startPos;
    public float maxDistance = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ÀË¬d­¸¦æ¶ZÂ÷
        float distance = Vector2.Distance(startPos, transform.position);

        if (distance >= maxDistance)
        {
            Destroy(gameObject);
            MonsterFarAttack.isPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy (gameObject);
        }
    }
}
