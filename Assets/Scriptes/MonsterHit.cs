using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float direction = 1;
    float speed = 0.5f;

    public Collider2D myHitCollider; // 連到 Box Collider


    //無敵時間設置
    public float noHitTime = 0.5f;
    public bool noHit = false;

    //移動設置
    float startX;
    public float moveRange = 1f; // 可在 Inspector 調整

    int hp = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        hp = 5;

        // 記錄初始位置
        startX = transform.position.x;
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        // 只對本 Collider 觸發受傷
        if (coll != myHitCollider) return;

        if (coll.CompareTag("closeAttack"))
        {
            hp -= 3;
            print(hp);
            Debug.Log("怪物-3");

            noHit = true;
            Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
        }
        if (coll.CompareTag("farAttack"))
        {
            hp -= 2;
            print(hp);

            Debug.Log("怪物-2");
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

        if (transform.position.x > startX + moveRange)
        {
            direction = -1;
            sr.flipX = false;
        }
        else if (transform.position.x < startX - moveRange) {
            direction = 1;
            sr.flipX=true;
        }

        
    }
}
