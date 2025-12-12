using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Awake()
    {
        // 找到玩家，把他移到這個位置
        if (!MonsterBossHit.bossdie)
        {
            if (GameManager.Instance != null && GameManager.Instance.Player != null)
            {
                GameManager.Instance.Player.transform.position = transform.position;
            }
        }
        
    }
}
