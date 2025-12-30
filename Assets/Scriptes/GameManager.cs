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
            // ?T?O?o???O GameManager ??b??????A?]???O Map ?????????? UI
            // ?p?G?A?? UI ???? DontDestroyOnLoad?A?o??N??????
            Destroy(ui);
        }
        if (Player != null) Destroy(Player);

        // --- (??l??P?O?@????) ---
        //UI????
        PlayerUI = Instantiate(PlayerUIPrefab);
        collectionGetInstance = PlayerUI.GetComponentInChildren<CollectionGet>();

        //???a????
        Player = Instantiate(PlayerPrefab);
        PlayerMovement pm = Player.GetComponent<PlayerMovement>();
        pm.CollectionGet = collectionGetInstance; // ???????

        // ?? HeartHp
        HeartHp heartHp = PlayerUI.GetComponentInChildren<HeartHp>();

        // ??T?{?A???????? TextMeshProUGUI ?????|

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
        // ???????O?A?n????]?B?i????????
        if ((scene.name == "GameApple"|| scene.name == "GameSugar" || scene.name == "NPC" || scene.name == "GameButter") && !MonsterBossHit.bossdie)
        {
            PlayerMovement pm = Player.GetComponent<PlayerMovement>();
            pm.PlayEnterAnimation();   //?I?s?J????e
        }
    }


}
