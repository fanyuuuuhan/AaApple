using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator ani;
    float direction = 1;
    float speed = 0.5f;
    public Transform player;


    //無敵時間設置
    public float noHitTime = 0.5f;
    public bool noHit = false;

    //移動設置
    float startX;
    public float moveRange = 1f; // 可在 Inspector 調整

    //停頓判斷
    public float stopDuration = 1f;  // 停頓時間
    public float AttackStop = 0.5f;
    public float HurtStop = 0.5f;
    bool isMoving = false;

    //特定怪物顯示傳送門
    public GameObject Portal;
    public bool controlsPortal = false; // 是否控制 Portal

    //鑰匙偵測
    public bool hasKey = false;
    public GameObject Key;

    int hp = 0;

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

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //行走停頓
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
    //攻擊停頓
    IEnumerator AttackPause()
    {
        ani.SetBool("attack", true);
        yield return new WaitForSeconds(AttackStop);
        ani.SetBool("attack", false);
    }
    //受傷停頓
    IEnumerator HurtPause()
    {
        ani.SetBool("hurt", true);
        yield return new WaitForSeconds(HurtStop);
        ani.SetBool("hurt", false);
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (CompareTag("FarMonster"))
        {
            if (coll.CompareTag("closeAttack") && coll.IsTouching(GetComponent<PolygonCollider2D>()))
            {
                hp -= 3;
                print(hp);
                Debug.Log("怪物-3");
                ani.SetBool("move", false);
                StartCoroutine(HurtPause());

                noHit = true;
                Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
            }
            if (coll.CompareTag("farAttack") && coll.IsTouching(GetComponent<PolygonCollider2D>()))
            {
                hp -= 2;
                print(hp);
                ani.SetBool("move", false);
                StartCoroutine(HurtPause());

                Debug.Log("怪物-2");
                noHit = true;
                Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
            }
        }
        else if(CompareTag("Monster"))
        {
            if (coll.CompareTag("closeAttack") && coll.IsTouching(GetComponent<BoxCollider2D>()))
            {
                hp -= 3;
                print(hp);
                Debug.Log("怪物-3");
                ani.SetBool("move", false);
                StartCoroutine(HurtPause());

                noHit = true;
                Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
            }
            if (coll.CompareTag("farAttack") && coll.IsTouching(GetComponent<BoxCollider2D>()))
            {
                hp -= 2;
                print(hp);
                ani.SetBool("move", false);
                StartCoroutine(HurtPause());

                Debug.Log("怪物-2");
                noHit = true;
                Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
            }
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
            if (controlsPortal && Portal != null)
                Portal.SetActive(true);
            if (hasKey && Key != null)
            {
                Key.SetActive(true);
            }
            Destroy(this.gameObject);
        }



        if (!isMoving) return;

       //遠攻怪物轉向
        if (CompareTag("FarMonster"))
        {
            //攻擊轉向
            if (MonsterFarAttack.isPlayer)
            {
                sr.flipX = player.position.x < -transform.position.x;
            }
            else
            {
                //非攻擊時：依移動方向翻面
                sr.flipX = direction < 0;
            }

            //攻擊動畫
            if (MonsterFarAttack.isAttack)
            {
                StartCoroutine(AttackPause());
                MonsterFarAttack.isAttack = false;
            }         

            //怪物移動
            transform.position += new Vector3(-speed * direction * Time.deltaTime, 0, 0);
            if (transform.position.x > startX + moveRange)
            {
                direction = 1;
            }
            else if (transform.position.x < startX - moveRange)
            {
                direction = -1;
            }
        }
                
    }
}
