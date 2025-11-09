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
    bool isflip = false;

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

    //攻擊偵測
    public GameObject weapon;      // 指定要旋轉的物件
    public float rotateSpeed = 200f; // 旋轉速度（度/秒）
    private bool rotatingToTarget = false;
    bool turnback = false;
    private Quaternion targetRotation;

    //丟假牙
    public GameObject Falsetooth;
    


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


    void FixedUpdate()
    {

        if (!isKnock)
        {
            if (Input.GetKey(KeyCode.D))
            {
                movement = speed;
                ani.SetBool("run", true);
                //旋轉整個物件的座標
                transform.localScale = new Vector3(1, 1, 1);
                isflip = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                movement = -speed;
                ani.SetBool("run", true);
                //旋轉整個物件的座標
                transform.localScale = new Vector3(-1, 1, 1);
                isflip = true;
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


        // 按下 M 鍵攻擊+旋轉
        if ((Input.GetKeyDown(KeyCode.M)||Input.GetMouseButtonDown(0)) && !rotatingToTarget && !turnback)
        {
            if (!isflip)
            {
                targetRotation = Quaternion.Euler(0, 0, -60);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, 0, 60);
            }
            weapon.SetActive(true);
            rotatingToTarget = true;

        }   
        if (rotatingToTarget)//平滑旋轉到目標角度
        {
            
            weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // 當旋轉接近目標時，停止旋轉
            if (Quaternion.Angle(weapon.transform.rotation, targetRotation) < 0.1f)
            {
                weapon.transform.rotation = targetRotation;
                rotatingToTarget = false;
                turnback = true;
            }
        }       
        if (turnback)//轉回去
        {

            if (!isflip)
            {
                targetRotation = Quaternion.Euler(0, 0, -20);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, 0, 20);
            }

            weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(weapon.transform.rotation, targetRotation) < 0.1f)
            {
                weapon.transform.rotation = targetRotation;
                turnback = false; // 完成回轉
                weapon.SetActive(false);
            }
        }
        //發射假牙
        if ((Input.GetKeyDown(KeyCode.K)||Input.GetMouseButtonDown(1)) && !TeethControl.isthrow)
        {
            GameObject tooth = Instantiate(Falsetooth, transform.position, Quaternion.identity);

            //改成用旋轉判定方向，而不是改 scale
            if (transform.localScale.x < 0)
            {
                tooth.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                tooth.transform.rotation = Quaternion.identity;
            }
                

            tooth.GetComponent<TeethControl>().player = this.transform;
            TeethControl.isthrow = true;
        }

    }


}
