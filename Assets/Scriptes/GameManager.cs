using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CollectionGet collectionGetInstance;
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

    public void Init()
    {
        GameObject[] oldUIs = GameObject.FindGameObjectsWithTag("Canvas");
        foreach (GameObject ui in oldUIs)
        {
            // 確保這不是 GameManager 所在的物件，也不是 Map 場景本身的 UI
            // 如果你的 UI 都有 DontDestroyOnLoad，這行就能抓到它們
            Destroy(ui);
        }
        if (Player != null) Destroy(Player);

        // --- (初始化與保護物件) ---
        //UI物件
        PlayerUI = Instantiate(PlayerUIPrefab);
        collectionGetInstance = PlayerUI.GetComponentInChildren<CollectionGet>();

        //玩家物件
        Player = Instantiate(PlayerPrefab);
        PlayerMovement pm = Player.GetComponent<PlayerMovement>();
        pm.CollectionGet = collectionGetInstance; // 直接引用

        // 抓 HeartHp
        HeartHp heartHp = PlayerUI.GetComponentInChildren<HeartHp>();

        // 請確認你的收集物 TextMeshProUGUI 的路徑

        if (heartHp != null)
        {
            pm.HeartHp = heartHp;
        }

        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(PlayerUI);
    }
    
    void Start()
    {

        

    }

    
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
        if ((scene.name == "GameApple"|| scene.name == "GameSugar") && !MonsterBossHit.bossdie)
        {
            PlayerMovement pm = Player.GetComponent<PlayerMovement>();
            pm.PlayEnterAnimation();   //呼叫入場動畫
        }
    }


}
