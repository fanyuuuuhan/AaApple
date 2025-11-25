using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer sr;

    //收集物件
    public int collection = 0;
    public TextMeshProUGUI CollCount;

    //角色移動
    public float jump = 2f;
    public float speed = 2f;
    float movement;
    bool isflip = false;

    //地面偵測
    bool isGround = false;

    //血量
    int HP = 20;
    public int max_hp = 0;
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

    //踩到奶油滑滑控制
    public float butterSpeed = 0.8f;
    public float butterLerp = 1.5f;
    bool isButter = false;

    //機器修復
    MachineFix textfix;
    bool isFixing = false;
    public Image CanvaFix;
    public Image FixBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rb= GetComponent<Rigidbody2D>();
        ani= GetComponent<Animator>();
        sr= GetComponent<SpriteRenderer>();

        max_hp = 20;
        HP = max_hp;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = true;
            ani.SetBool("jump", false);
        }
        //收集物件
        if (collision.tag == "bling")
        {
            Destroy(collision.gameObject);
            collection++;
            CollCount.text = $"{collection:F0}";
        }
        //轉移場景
        if (collision.CompareTag("AppleGo"))
        {
            Portal portal = collision.GetComponent<Portal>();
            if (portal != null)
            {
                portal.SceneChange();
                transform.position = new Vector3(-7, 6 , 0);
            }
        }
        //奶油偵測
        if (collision.CompareTag("Butter"))
        {
            isButter = true;
        }
        //機器偵測
        if (collision.CompareTag("Machine"))
        {
            textfix = collision.GetComponent<MachineFix>();
            if (textfix != null && textfix.isFixable) //MachineFix確認有啟動並且機器可以修理
            {
                Debug.Log("機器偵測");
                textfix.ShowText();
            }
            else
            {
                textfix.FixOver.SetActive(true);
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = false;
        }
        if (collision.CompareTag("Butter"))
        {
            isButter = false;
        }
        if (collision.CompareTag("Machine") && textfix != null)
        {
            Debug.Log("機器偵測離開");
            textfix.HideText();
            textfix = null;
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
            if (!isButter)
            {
                rb.linearVelocityX = movement;
            }
            else
            {
                float target = movement * butterSpeed;
                rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, target, Time.deltaTime * butterLerp); //平滑過度速度
            }
            

            if (Input.GetKey(KeyCode.Space) && isGround == true)
            {
                rb.linearVelocity = new Vector2(movement, jump);
                ani.SetBool("jump", true);
            }            
        }

        
    }

    void Update()
    {
        HPbar.transform.localScale = new Vector3((float)HP / (float)max_hp, HPbar.transform.localScale.y, HPbar.transform.localScale.z);

        
        //修理機器
        if (textfix != null && textfix.isFixable && !isFixing)
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetMouseButtonDown(0))
            {
                StartCoroutine(FixMachine());//等待3秒、撥放修理動畫
            }
        }
        // 按下 M 鍵攻擊+旋轉
        else
        {
            if ((Input.GetKeyDown(KeyCode.M) || Input.GetMouseButtonDown(0)) && !rotatingToTarget && !turnback)
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
            if ((Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(1)) && !TeethControl.isthrow)
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


    IEnumerator FixMachine()
    {
        isFixing = true;

        // 停止玩家所有控制
        float oldSpeed = speed;
        float oldJump = jump;
        speed = 0;
        jump = 0;
        rb.linearVelocity = Vector2.zero;

        if(textfix!=null)
        {
            textfix.StartFix();
            CanvaFix.gameObject.SetActive(true);
            FixBar.gameObject.SetActive(true);
        }


        HPbar.transform.localScale = new Vector3((float)HP / (float)max_hp, HPbar.transform.localScale.y, HPbar.transform.localScale.z);

        float fixTime = 3f;   // 修理總時間
        float currentFix = 0f; // 當前修理進度 0→fixTime

        // 假設 FixBar 原本 scale.x = 0，最終 1
        Vector3 startScale = FixBar.transform.localScale;
        Vector3 endScale = new Vector3(1f, startScale.y, startScale.z);

        while (currentFix < fixTime)
        {
            currentFix += Time.deltaTime;

            // 直接用比例算 X 軸
            float ratio = Mathf.Clamp01(currentFix / fixTime);
            FixBar.transform.localScale = new Vector3(ratio, FixBar.transform.localScale.y, FixBar.transform.localScale.z);

            yield return null;
        }

        // 確保最後填滿
        FixBar.transform.localScale = endScale;

        CanvaFix.gameObject.SetActive(false);
        FixBar.gameObject.SetActive(false);

        // 恢復移動能力
        speed = oldSpeed;
        jump = oldJump;

        isFixing = false;
        textfix.isFixable = false;
    }
}
