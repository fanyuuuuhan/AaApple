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
        Transform hpBarTransform = PlayerUI.transform.Find("Image-HP/HPbar");
        Transform fixBarTransform = PlayerUI.transform.Find("FixBar");
        // 請確認你的收集物 TextMeshProUGUI 的路徑
        Transform collCountTextTransform = PlayerUI.transform.Find("CollectionCount");

        if (hpBarTransform != null)
        {
            PlayerMovement.HPbar = hpBarTransform.GetComponent<Image>();
        }
        if (fixBarTransform != null)
        {
            PlayerMovement.FixBar = fixBarTransform.GetComponent<Image>();
        }
        if (collCountTextTransform != null)
        {
            PlayerMovement.CollCount = collCountTextTransform.GetComponent<TextMeshProUGUI>();
        }

        // 檢查 UI 是否成功連接
        if (PlayerMovement.HPbar == null || PlayerMovement.CollCount == null)
        {
            Debug.LogError("UI 連接失敗！請檢查 PlayerUIPrefab 裡的組件路徑是否正確。");
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


}
