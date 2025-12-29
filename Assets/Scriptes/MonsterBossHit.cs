using System.Collections;
using UnityEngine;
using static MonsterHit;

public class MonsterBossHit : MonoBehaviour
{
    public GameObject Achievement;

    //無敵時間設置
    public float noHitTime = 0.5f;
    public bool noHit = false;

    //Boss死亡
    public static bool bossdie=false;
    public string monsterID;

    public float HurtStop = 0.5f;

    Animator ani;

    //受傷停頓
    IEnumerator HurtPause()
    {
        ani.SetBool("hurt", true);
        yield return new WaitForSeconds(HurtStop);
        ani.SetBool("hurt", false);
    }

    int hp = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        hp = 20;

        //怪物是否已死亡紀錄
        if (MonsterDie.DeadMonster.Contains(monsterID))
        {
            gameObject.SetActive(false); // 如果已經死過，直接隱藏
        }

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (noHit) return;   // ← 必加！

        if ((coll.CompareTag("closeAttack")|| coll.CompareTag("farAttack")) && coll.IsTouching(GetComponent<PolygonCollider2D>()))
        {
            hp -= 1;
            print(hp);
            Debug.Log("Boss-1");

            StartCoroutine(HurtPause());

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
            if (!string.IsNullOrEmpty(monsterID))
            {
                MonsterDie.DeadMonster.Add(monsterID);
            }

            bossdie = true;
            Destroy(this.gameObject);
            Achievement.SetActive(true);
        }

    }
}
