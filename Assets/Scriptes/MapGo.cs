using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; //UI用的scripts

public class MapUIClickHandler : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string sceneName;

    //hover放大
    float hoverspeed = 8f;
    public float hoverscale = 1.5f;
    bool ishover = false;
    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        GameObject player = GameObject.Find("Player");
        Destroy(player);
    }

    public void OnPointerClick(PointerEventData eventData) //點擊事件
    {
        PlayerData.NextSceneName = sceneName;

        if (sceneName == "GameApple")
        {
            SceneManager.LoadScene("Init");
        }
        if (sceneName == "GameSugar")
        {
            SceneManager.LoadScene("Init2");
        }
        if (sceneName == "GameButter" && MapUnlock.isMapUnlock)
        {
            SceneManager.LoadScene("Init3");
        }
        else if (!MapUnlock.isMapUnlock) 
        {
            return;
        }

    }

    void Update()
    {
        GameObject[] oldUIs = GameObject.FindGameObjectsWithTag("Manager");
        foreach (GameObject ui in oldUIs)
        {
            // 確保這不是 GameManager 所在的物件，也不是 Map 場景本身的 UI
            // 如果你的 UI 都有 DontDestroyOnLoad，這行就能抓到它們
            Destroy(ui);
        }

        if (ishover)
        {
            // 放大
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale * hoverscale,
                Time.deltaTime * hoverspeed
            );
        }
        else
        {
            // 還原
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale,
                Time.deltaTime * hoverspeed
            );
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ishover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ishover = false;
    }
}
