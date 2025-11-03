using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer sr;

    //收集物件
    public int collection = 0;

    //角色移動
    public float jump = 2f;
    public float speed = 2f;
    float movement;

    //地面偵測
    bool isGround = false;

    //血量
    public int HP = 0;
    int max_hp = 0;
    public Image HPbar;

    //無敵時間設置
    public float noHitTime = 0.5f;
    bool noHit=false;

    //受傷反彈
    public float knockback = 5f;
    bool isKnock = false;
    public float knockTime = 0.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        ani= GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>();
        
        max_hp = 20;
        HP = max_hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = true;
            ani.SetBool("jump", false);
        }
        if (collision.tag == "bling")
        {
            Destroy(collision.gameObject);
            collection++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Monster"&&!noHit)
        {
            print(coll.gameObject.name);
            HP -= 1;
            

            noHit = true;
            Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵

            //手傷害後反彈
            isKnock = true;
            Invoke(nameof(ResetKnock), knockTime);
            rb.linearVelocity = new Vector2((transform.position.x < coll.transform.position.x ? -1 : 1) * knockback, rb.linearVelocity.y);
        }
    }

    void ResetHit()
    {
        noHit = false;
    }

    void ResetKnock()
    {
        isKnock = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isKnock)
        {
            if (Input.GetKey(KeyCode.D))
            {
                movement = speed;
                ani.SetBool("run", true);

                sr.flipX = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement = -speed;
                ani.SetBool("run", true);

                sr.flipX = true;
            }
            else
            {
                movement = 0f;
                ani.SetBool("run", false);
            }
            rb.linearVelocityX = movement;

            if (Input.GetKey(KeyCode.Space) && isGround == true)
            {
                rb.linearVelocity = new Vector2(movement, jump);
                ani.SetBool("jump", true);
            }
        }
        
    }

    private void Update()
    {
        HPbar.transform.localScale = new Vector3((float)HP / (float)max_hp, HPbar.transform.localScale.y, HPbar.transform.localScale.z);
    }


}
