using UnityEngine;

public class Achievement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterBossHit.bossdie)
        {
            this.gameObject.SetActive(true);
        }
        if (PlayerMovement.isAch)
        {
            Destroy(gameObject);
        }
    }
}
