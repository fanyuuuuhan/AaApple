using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float nowTimeS; //計時器秒數個位
    public TextMeshProUGUI timerS;
    public static float nowTimeM; //計時器分數個位
    public TextMeshProUGUI timerM;
    public static float Stimer; //計時器秒數十位
    public TextMeshProUGUI S0;
    public static float Mtimer; //計時器分數十位
    public TextMeshProUGUI M0;

    public GameObject EndStar;
    public GameObject GameOver;

    public float GetNowTimeS() => nowTimeS;
    public float GetNowTimeM() => nowTimeM;

    //星星顯示
    public bool StarShow = false;

    void Start()
    {
        // 如果是從 Portal 返回 → 從 PlayerData 繼續
        nowTimeS = PlayerData.PauseTimeS;
        nowTimeM = PlayerData.PauseTimeM;
        Stimer = PlayerData.PauseSTime;
        Mtimer = PlayerData.PauseMTime;

        // 回來後要恢復計時
        PlayerData.TimerPaused = false;
    }

    void Update()
    {
        // 若計時器被暫停（跨場景時）就完全不累加
        if (PlayerData.TimerPaused)
            return;

        nowTimeS += Time.deltaTime;

        if (nowTimeS >= 10)
        {
            nowTimeS = 0;
            Stimer++;
        }
        if (Stimer >= 6) 
        {
            Stimer = 0;
            nowTimeM++;
        }
        if (nowTimeM >= 10)
        {
            nowTimeM = 0;
            Mtimer++;
        }


        timerS.text = Mathf.Floor(nowTimeS).ToString();
        timerM.text = Mathf.Floor(nowTimeM).ToString();
        S0.text = Mathf.Floor(Stimer).ToString();
        M0.text = $"{Mtimer}";

        //完成遊戲
        if (PlayerMovement.isEndStar)
        {
            PlayerData.TimerPaused = true;
            EndStar.gameObject.SetActive(true);
            StarShow = true;
        }

        //遊戲死亡
        if (PlayerMovement.isGameOver)
        {
            PlayerData.TimerPaused = true;
            GameOver.gameObject.SetActive(true);

        }
    }


    void OnDestroy()
    {
        // 離開場景保存時間
        PlayerData.PauseTimeS = nowTimeS;
        PlayerData.PauseTimeM = nowTimeM;
        PlayerData.PauseSTime = Stimer;
        PlayerData.PauseMTime = Mtimer;
    }

}
