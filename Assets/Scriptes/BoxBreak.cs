using UnityEngine;
using System.Collections;

public class BoxBreak : MonoBehaviour
{
    Animator ani;
    public int isBreak = 0;

    public float noHitTime = 0.5f;
    public bool noHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("closeAttack") || collision.CompareTag("farAttack"))
        {
            isBreak++;
            noHit = true;
            Invoke(nameof(ResetHit), noHitTime); // 自動在 noHitTime 秒後解除無敵
        }
    }

    void ResetHit()
    {
        noHit = false;
    }

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetInteger("BeBreak", isBreak);
        if (isBreak >= 2)
        {
            StartCoroutine(BoxBeBreak());
        }
    }



    IEnumerator BoxBeBreak()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
