using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static string NextSceneName = ""; //Map時的場景
    public static string RestartScene = "";   // Restart 時要回的場景
    public static string PorScene = ""; //傳送的場景
    public static string NowScene = ""; //Map返回
    public static string BackScene = "";

    //計時器
    public static float PauseTimeS = 0;
    public static float PauseTimeM = 0;
    public static float PauseSTime = 0;
    public static float PauseMTime = 0;
    public static bool TimerPaused = false;

    //總星星數
    public static float AllOfStar = 0;

}
