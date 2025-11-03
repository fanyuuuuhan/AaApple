using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float direction = 1;
    float speed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.flipX = true;
        transform.position += new Vector3(speed * direction * Time.deltaTime, 0, 0);

        if (transform.position.x > 5f)
        {
            direction = -1;
            sr.flipX = false;
        }
        else if (transform.position.x < 3f) {
            direction = 1;
            sr.flipX=true;
        }
    }
}
