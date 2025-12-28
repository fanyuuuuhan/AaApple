using UnityEngine;

public class Achievement : MonoBehaviour
{
    public string cardKey; // 例如 "Card_Sword"

    // 玩家收集成就時呼叫這個方法
    public void UnlockCard()
    {
        if (!string.IsNullOrEmpty(cardKey))
        {
            PlayerPrefs.SetInt(cardKey, 1);
            PlayerPrefs.Save();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterBossHit.bossdie)
        {
            gameObject.SetActive(true);
            GetComponent<SpriteRenderer>().enabled = true;
        }
        if (PlayerMovement.isAch)
        {
            Destroy(gameObject);
        }
    }
}
