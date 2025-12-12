using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitPlayer : MonoBehaviour
{
    public static int HP = 20;
    public static int maxHP = 20;
    public static int collection = 0;

    public static bool isBackMap = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayerData.NextSceneName != "Map"|| PlayerData.RestartScene != "Map" || PlayerData.PorScene != null)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return null;

        //Restart 時用 RestartScene
        if (!string.IsNullOrEmpty(PlayerData.RestartScene))
        {
            string target = PlayerData.RestartScene;
            PlayerData.RestartScene = ""; // 用完清空
            SceneManager.LoadScene(target);
            yield break;
        }

        //一般 Map 按鈕選擇用 NextSceneName
        if (!string.IsNullOrEmpty(PlayerData.NextSceneName))
        {
            string target = PlayerData.NextSceneName;
            PlayerData.NextSceneName = ""; // 用完清空
            SceneManager.LoadScene(target);
            if (target == "Map")
            {
                isBackMap = true;
            }
        }
    }
}
