using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string AsceneName;

    void Update()
    {
        if (MonsterBossHit.bossdie)
        {
            Destroy(gameObject);
        }
    }
}
