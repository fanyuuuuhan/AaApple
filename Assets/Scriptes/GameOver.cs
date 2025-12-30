using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public Timer Timer;
    public TextMeshProUGUI timerS;//璸竟计
    public TextMeshProUGUI timerM;//璸竟だ计
    public TextMeshProUGUI S0;//璸竟计
    public TextMeshProUGUI M0;//璸竟だ计

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timerS.text = Mathf.Floor(Timer.nowTimeS).ToString();
        timerM.text = Mathf.Floor(Timer.nowTimeM).ToString();
        S0.text = Mathf.Floor(Timer.Stimer).ToString();
        M0.text = $"{Timer.Mtimer}";
    }
}
