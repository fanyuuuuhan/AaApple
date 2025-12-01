using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC : MonoBehaviour
{
    public GameObject eSC;
    public GameObject setting;
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
            DontDestroyOnLoad(eSC);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            eSC.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
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
        Time.timeScale = 1f;
        
        PlayerData.RestartScene = SceneManager.GetActiveScene().name;
        Debug.Log("Restart：記錄的 RestartScene = " + PlayerData.RestartScene);

        InitPlayer.HP = 20;
        InitPlayer.maxHP = 20;
        InitPlayer.collection = 0;

        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            PlayerMovement pm = GameManager.Instance.Player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.HP = InitPlayer.HP;
                pm.max_hp = InitPlayer.maxHP;
                PlayerMovement.collection = InitPlayer.collection;

                //重設 UI 顯示
                if (PlayerMovement.HPbar != null)
                {
                    PlayerMovement.HPbar.transform.localScale =
                        new Vector3(1f,
                        PlayerMovement.HPbar.transform.localScale.y,
                        PlayerMovement.HPbar.transform.localScale.z);
                }

                if (PlayerMovement.CollCount != null)
                {
                    PlayerMovement.CollCount.text = "0";
                }
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

        //重置UI
        InitPlayer.HP = 20;
        InitPlayer.maxHP = 20;
        InitPlayer.collection = 0;
        if (GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            PlayerMovement pm = GameManager.Instance.Player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.HP = InitPlayer.HP;
                pm.max_hp = InitPlayer.maxHP;
                PlayerMovement.collection = InitPlayer.collection;

                //重設 UI 顯示
                if (PlayerMovement.HPbar != null)
                {
                    PlayerMovement.HPbar.transform.localScale =
                        new Vector3(1f,
                        PlayerMovement.HPbar.transform.localScale.y,
                        PlayerMovement.HPbar.transform.localScale.z);
                }

                if (PlayerMovement.CollCount != null)
                {
                    PlayerMovement.CollCount.text = "0";
                }
            }
        }

        Destroy(eSC);
    }
}
