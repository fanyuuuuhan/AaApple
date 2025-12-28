using UnityEngine;

public class ThingBreak : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        if (MonsterBossHit.bossdie)
        {
            Destroy(gameObject);
        }
    }
}
