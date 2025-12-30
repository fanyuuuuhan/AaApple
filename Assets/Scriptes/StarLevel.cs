using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StarLevel : MonoBehaviour
{
    public Timer Timer;
    public Image[] star;

    public TextMeshProUGUI timerS;//計時器秒數個位
    public TextMeshProUGUI timerM;//計時器分數個位
    public TextMeshProUGUI S0;//計時器秒數十位
    public TextMeshProUGUI M0;//計時器分數十位

    public TextMeshProUGUI Collection;



    public float Level1 = 0;
    public float Level2 = 0;
    public float Level3 = 0;

    public float coll1;
    public float coll2;
    public float coll3;

    public static bool isStar = false;

    //map星星數量顯示
    public string Gamekey;

    void Start()
    {

    }



    void Update()
    {
        if (Timer.StarShow)
        {
            Timer.StarShow = false;
            StartCoroutine(DelayAction());
            
        }
        timerS.text = Mathf.Floor(Timer.nowTimeS).ToString();
        timerM.text = Mathf.Floor(Timer.nowTimeM).ToString();
        S0.text = Mathf.Floor(Timer.Stimer).ToString();
        M0.text = $"{Timer.Mtimer}";
        Collection.text = $"{PlayerMovement.collection:f0}";
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(1f);  // 等 1 秒
        int starsEarned = 0;
        if (Timer.nowTimeM < Level1 && PlayerMovement.collection==coll1 )
        {
            star[0].gameObject.SetActive(true);
            star[1].gameObject.SetActive(true);
            star[2].gameObject.SetActive(true);
            isStar = true;
            starsEarned = 3;
        }
        else if (Timer.nowTimeM < Level2 && (PlayerMovement.collection <= coll1 && PlayerMovement.collection >= coll2 ))
        {
            star[0].gameObject.SetActive(true);
            star[1].gameObject.SetActive(true);
            isStar = true;
            starsEarned = 2;
        }
        else if (Timer.nowTimeM < Level3 && (PlayerMovement.collection <= coll1 && PlayerMovement.collection >= coll3))
        {
            star[0].gameObject.SetActive(true);
            isStar = true;
            starsEarned = 1;
        }
        else
        {
            isStar = false;
            starsEarned = 0;
        }

        // --- 新增：儲存最高紀錄 ---
        // 先讀取舊紀錄，確保不會因為重玩跑得更慢而蓋掉原本的三星紀錄
        int currentHighScore = PlayerPrefs.GetInt(Gamekey, 0);
        if (starsEarned > currentHighScore)
        {
            PlayerPrefs.SetInt(Gamekey, starsEarned);
            PlayerPrefs.Save(); // 強制存檔
        }
    }
}

