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

    //UI移動法
    RectTransform rect;
    public float speed = 50;
    bool StarMove = false;

    public float Level1 = 0;
    public float Level2 = 0;
    public float Level3 = 0;

    public static bool isStar = false;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }



    void Update()
    {
        if (Timer.StarShow)
        {
            Timer.StarShow = false;
            StarMove = true;
            
        }
        if (StarMove)
        {
            if (rect.anchoredPosition.y > 0)
            {
                rect.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);
            }
            else if (rect.anchoredPosition.y < 0)
            {
                rect.anchoredPosition = Vector2.zero;
                StarMove = false;
                StartCoroutine(DelayAction());
            }
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

        if (Timer.nowTimeM < Level1)
        {
            star[0].gameObject.SetActive(true);
            star[1].gameObject.SetActive(true);
            star[2].gameObject.SetActive(true);
            isStar = true;
        }
        else if (Timer.nowTimeM < Level2)
        {
            star[0].gameObject.SetActive(true);
            star[1].gameObject.SetActive(true);
            isStar = true;
        }
        else if (Timer.nowTimeM < Level3)
        {
            star[0].gameObject.SetActive(true);
            isStar = true;
        }
        else
        {
            isStar = false;
        }
    }
}

