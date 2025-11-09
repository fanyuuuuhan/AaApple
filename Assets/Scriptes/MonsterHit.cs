using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float direction = 1;
    float speed = 0.5f;

    //無敵時間設置
    public float noHitTime = 0.5f;
    bool noHit = false;

    int hp = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        hp = 5;
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "closeAttack")
        {
            hp -= 3;
            print(hp);

            noHit = true;
            Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
        }
        if (coll.gameObject.tag == "farAttack")
        {
            hp -= 2;
            print(hp);

            noHit = true;
            Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
        }
    }

    void ResetHit()
    {
        noHit = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

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
