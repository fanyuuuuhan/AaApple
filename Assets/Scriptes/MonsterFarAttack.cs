using UnityEngine;
using UnityEngine.UIElements;

public class MonsterFarAttack : MonoBehaviour
{
    
    public Transform player;

    public GameObject leaves;
    public Transform leavespoint; //子彈發射點
    public float delayTime = 1f; //子彈發射間隔時間
    public float speed = 2f; //子彈速度
    float timer;
    public static bool isPlayer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //追蹤角色位置
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            isPlayer = true;

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            isPlayer = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            //計算角色方位
            Vector3 dir = (player.position - leavespoint.position).normalized;

            // 轉向玩家
            float angle = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;  //Atan計算「方向向量的角度」，Rad2Deg弧度 → 角度 的轉換縮放
            transform.rotation = Quaternion.Euler(angle, 0, 0);

            // 計時射擊
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                timer = 0;

                GameObject bullet = Instantiate(leaves, leavespoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * speed; //dir=方向
            }
        }
    }

}

