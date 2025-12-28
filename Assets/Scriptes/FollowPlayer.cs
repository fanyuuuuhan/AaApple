using UnityEngine;
using UnityEngine.EventSystems;

public class FollowPlayer : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    float direction = 0;

    public Transform player;
    public float flySpeed = 4f;

    //移動緩衝區
    public float bound = 0.5f; // 玩家需要超過中心線 0.5 單位才會觸發翻轉
    public float stopDis = 0.2f; // 怪物與玩家過近時停止移動，防止重疊抖動

    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero; // 沒玩家就停下
            return;
        }
        float distanceX = player.position.x - transform.position.x;
        direction = distanceX > 0 ? 1f : -1f;


       //Mathf.Abs是絕對值的意思
       //移動邏輯
       if (Mathf.Abs(distanceX) > stopDis)
       {
           rb.linearVelocity = new Vector2(direction * flySpeed, 0);
       }
        else
        {
            // 到達玩家位置時完全靜止，防止滑行
            rb.linearVelocity = Vector2.zero;
        }
        
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        float distanceX = player.position.x - transform.position.x;

        //Mathf.Abs是絕對值的意思

        if (Mathf.Abs(distanceX) > bound)
        {
            if (distanceX > 0)
            {
                // 玩家在右邊 -> 怪物需要面右 -> 原圖是面左，所以要翻轉
                sr.flipX = true;
            }
            else
            {
                // 玩家在左邊 -> 怪物需要面左 -> 原圖是面左，所以不翻轉
                sr.flipX = false;
            }
        }


    }
}
