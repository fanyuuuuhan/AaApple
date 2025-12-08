using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float nowTimeS;
    public TextMeshProUGUI timerS;
    public float nowTimeM;
    public TextMeshProUGUI timerM;

    public float GetNowTimeS() => nowTimeS;
    public float GetNowTimeM() => nowTimeM;

    void Start()
    {
        // 如果是從 Portal 返回 → 從 PlayerData 繼續
        nowTimeS = PlayerData.PauseTimeS;
        nowTimeM = PlayerData.PauseTimeM;

        // 回來後要恢復計時
        PlayerData.TimerPaused = false;
    }

    void Update()
    {
        // 若計時器被暫停（跨場景時）就完全不累加
        if (PlayerData.TimerPaused)
            return;

        nowTimeS += Time.deltaTime;

        if (nowTimeS >= 60)
        {
            nowTimeS = 0;
            nowTimeM++;
        }

        timerS.text = $"{nowTimeS:F0}";
        timerM.text = $"{nowTimeM:F0}";
    }

    void OnDestroy()
    {
        // 離開場景保存時間
        PlayerData.PauseTimeS = nowTimeS;
        PlayerData.PauseTimeM = nowTimeM;
    }
}
