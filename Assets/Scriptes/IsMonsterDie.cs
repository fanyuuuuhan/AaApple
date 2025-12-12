using UnityEngine;

public class IsMonsterDie : MonoBehaviour
{
    public static bool isDie = false;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject == null && MonsterBossHit.bossdie)
        {
            isDie = true;
        }

        if (isDie)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
