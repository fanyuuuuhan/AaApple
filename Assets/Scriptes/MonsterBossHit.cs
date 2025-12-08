using UnityEngine;

public class MonsterBossHit : MonoBehaviour
{


    //無敵時間設置
    public float noHitTime = 0.5f;
    public bool noHit = false;

    //Boss死亡
    public static bool bossdie=false;


    int hp = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        hp = 20;

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (noHit) return;   // ← 必加！

        if ((coll.CompareTag("closeAttack")|| coll.CompareTag("farAttack")) && coll.IsTouching(GetComponent<BoxCollider2D>()))
        {
            hp -= 3;
            print(hp);
            Debug.Log("Boss-1");

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
            bossdie = true;
            Destroy(this.gameObject);
            
        }

    }
}
