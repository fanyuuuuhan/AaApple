using UnityEngine;

public class TeethControl : MonoBehaviour
{
    public Transform player; // 主角
    public float speed = 5f; // 飛行速度
    public float maxDistance = 8f; // 飛多遠（0.5代表半公尺）
    private Vector3 startPos;
    private bool returning = false;
    public static bool isthrow = false;


    Animator playAni;

    void Start()
    {
        startPos = transform.position;
        isthrow = true;
        playAni = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (!returning)
        {
            // 往前飛（根據角色面向方向）
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // 超過距離就回頭
            if (Vector3.Distance(startPos, transform.position) >= maxDistance)
                returning = true;

        }
        else
        {
            // 飛回主角
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // 回到主角附近後刪除
            if (Vector3.Distance(transform.position, player.position) < 0.05f)
            {
                isthrow = false;
                playAni.SetBool("attack", false);
                Destroy(gameObject);
            }
        }
    }
}
