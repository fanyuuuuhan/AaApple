using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    //UI移動法
    RectTransform rect;
    public float speed = 50;

    public Timer Timer;
    public TextMeshProUGUI timerS;//計時器秒數個位
    public TextMeshProUGUI timerM;//計時器分數個位
    public TextMeshProUGUI S0;//計時器秒數十位
    public TextMeshProUGUI M0;//計時器分數十位

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rect.anchoredPosition.y < 0)
        {
            rect.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
        }
        else if (rect.anchoredPosition.y > 0)
        {
            rect.anchoredPosition = Vector2.zero;
        }

        timerS.text = Mathf.Floor(Timer.nowTimeS).ToString();
        timerM.text = Mathf.Floor(Timer.nowTimeM).ToString();
        S0.text = Mathf.Floor(Timer.Stimer).ToString();
        M0.text = $"{Timer.Mtimer}";
    }
}
