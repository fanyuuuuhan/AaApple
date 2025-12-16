using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class MonsterFarAttack : MonoBehaviour
{
    
    public Transform player;

    public GameObject Bullet;
    public Transform Bulletpoint; //子彈發射點
    public float delayTime = 1f; //子彈發射間隔時間
    public float speed = 2f; //子彈速度
    float timer;
    public static bool isPlayer = false;
    public static bool isAttack = false;

    private void Awake()
    {

    }

    IEnumerator Start()
    {
        // 等待 Player 生成
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }
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
            Vector3 dir = (player.position - Bulletpoint.position).normalized;

            // 計時射擊
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                timer = 0;

                isAttack = true;
                GameObject bullet = Instantiate(Bullet, Bulletpoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * speed; //dir=方向
            }
        }
    }

}

