using UnityEngine;
using System.Collections;

public class MonsterHit : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator ani;
    float direction = 1;
    float speed = 0.5f;


    //無敵時間設置
    public float noHitTime = 0.5f;
    public bool noHit = false;

    //移動設置
    float startX;
    public float moveRange = 1f; // 可在 Inspector 調整

    //停頓判斷
    public float stopDuration = 1f;  // 停頓時間
    bool isMoving = false;

    int hp = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        hp = 5;

        // 記錄初始位置
        startX = transform.position.x;
        
        //停頓動畫
        StartCoroutine(MoveWithPause());
    }

    IEnumerator MoveWithPause()
    {
        while (true)
        {
            // 停頓 1 秒
            isMoving = false;
            ani.SetBool("move", false);
            yield return new WaitForSeconds(stopDuration);

            // 移動 2 秒（可自由調整）
            isMoving = true;
            ani.SetBool("move", true);
            yield return new WaitForSeconds(2f);

            
        }
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.CompareTag("closeAttack") && coll.IsTouching(GetComponent<BoxCollider2D>()))
        {
            hp -= 3;
            print(hp);
            Debug.Log("怪物-3");
            ani.SetBool("move", false);

            noHit = true;
            Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
        }
        if (coll.CompareTag("farAttack") && coll.IsTouching(GetComponent<BoxCollider2D>()))
        {
            hp -= 2;
            print(hp);
            ani.SetBool("move", false);

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
        ani.SetBool("move", true);


        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        if (!isMoving) return;

       
        if (CompareTag("FarMonster"))
        {
            
            transform.position += new Vector3(-speed * direction * Time.deltaTime, 0, 0);
            if (transform.position.x > startX + moveRange)
            {
                direction = 1;
                sr.flipX = false;
            }
            else if (transform.position.x < startX - moveRange)
            {
                direction = -1;
                sr.flipX = true;
            }
        }
        

        
    }
}
