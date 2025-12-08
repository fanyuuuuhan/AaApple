using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager Instance;

    public GameObject Player;
    public GameObject PlayerUI;
    public GameObject PlayerPrefab;
    public GameObject PlayerUIPrefab;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // --- (初始化與保護物件) ---
        Player = Instantiate(PlayerPrefab);
        PlayerUI = Instantiate(PlayerUIPrefab);

        // 確保你的 PlayerUI Prefab 中有這些路徑和組件
        PlayerMovement pm = Player.GetComponent<PlayerMovement>();

        // 抓 HeartHp
        HeartHp heartHp = PlayerUI.GetComponentInChildren<HeartHp>();
        
        // 請確認你的收集物 TextMeshProUGUI 的路徑
        Transform collCountTextTransform = PlayerUI.transform.Find("CollectionCount");

        if (heartHp != null)
        {
            pm.HeartHp = heartHp;
        }
        if (collCountTextTransform != null)
        {
            PlayerMovement.CollCount = collCountTextTransform.GetComponent<TextMeshProUGUI>();
        }

        DontDestroyOnLoad(Player);

    }

    // Update is called once per frame
    void Update()
    {
        if (InitPlayer.isBackMap)
        {
            PlayerData.NowScene = SceneManager.GetActiveScene().name;
            if (PlayerData.NowScene == "Map")
            {
                InitPlayer.isBackMap = false;
                PlayerData.NowScene = "";
            }
        }
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 當場景是你要播放跑步進場的場景
        if (scene.name == "GameApple")
        {
            PlayerMovement pm = Player.GetComponent<PlayerMovement>();
            pm.PlayEnterAnimation();   //呼叫入場動畫
        }
    }


}
