using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string AsceneName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (PlayerMovement.isPor)
        //{
        //    PlayerData.PorScene = AsceneName;
        //    GameManager.Instance.SceneChange(AsceneName);
        //    Debug.Log(AsceneName + "¤w½ò¨ì");
        //    PlayerMovement.isPor = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterBossHit.bossdie)
        {
            Destroy(gameObject);
        }
    }
}
