using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    public GameObject eSC;
    public GameObject setting;
    Timer Timer;

    public static bool isRestart = false;

    private void Awake()
    {

    }

    void Start()
    {
        
        GameObject[] canvases = GameObject.FindGameObjectsWithTag("Canvas");

        if (canvases.Length > 1)
        {
            Destroy(gameObject);  // 刪除多餘的 Canvas
        }
        else
        {
           // DontDestroyOnLoad(eSC);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            eSC.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        Timer = FindAnyObjectByType<Timer>();
    }

    public void ContiuneGame()
    {
        eSC.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        isRestart = true;
        eSC.gameObject.SetActive(false);
        Timer.EndStar.gameObject.SetActive(false);
        Timer.GameOver.gameObject.SetActive(false);
        Time.timeScale = 1f;
        MonsterBossHit.bossdie = false;
        
        PlayerData.RestartScene = SceneManager.GetActiveScene().name;
        Debug.Log("Restart：記錄的 RestartScene = " + PlayerData.RestartScene);

        InitPlayer.HP = 20;
        InitPlayer.maxHP = 20;
        Timer.nowTimeS = 0;
        Timer.nowTimeM = 0;
        Timer.Stimer = 0;
        Timer.Mtimer = 0;
        PlayerData.TimerPaused = false;
        PlayerMovement.isEndStar = false;
        PlayerMovement.isGameOver = false;

        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            PlayerMovement pm = GameManager.Instance.Player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                PlayerMovement.HP = InitPlayer.HP;
                pm.max_hp = InitPlayer.maxHP;

                //正確重設愛心血量
                if (pm.HeartHp != null)
                    pm.HeartHp.UpdateHearts(PlayerMovement.HP, pm.max_hp);

            }
        }

        SceneManager.LoadScene("Init");
    }

    public void Setting()
    {
        setting.gameObject.SetActive(true);
    }

    public void BackMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
        eSC.gameObject.SetActive(false);
        Timer.EndStar.gameObject.SetActive(false);
        Timer.GameOver.gameObject.SetActive(false);
        PlayerMovement.isEndStar = false;
        PlayerMovement.isGameOver = false;
        MonsterBossHit.bossdie = false;


        //重置UI
        InitPlayer.HP = 20;
        InitPlayer.maxHP = 20;
        Timer.nowTimeS = 0;
        Timer.nowTimeM = 0;
        Timer.Stimer = 0;
        Timer.Mtimer = 0;
        PlayerData.TimerPaused = false;
        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            PlayerMovement pm = GameManager.Instance.Player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                PlayerMovement.HP = InitPlayer.HP;
                pm.max_hp = InitPlayer.maxHP;

                //正確重設愛心血量
                if (pm.HeartHp != null)
                    pm.HeartHp.UpdateHearts(PlayerMovement.HP, pm.max_hp);

            }
        }

    }

    public void CloseSetting()
    {
        setting.gameObject.SetActive(false);
    }

}
