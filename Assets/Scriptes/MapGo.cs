using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems; //UI???scripts

public class MapUIClickHandler : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string sceneName;

    //hover??j
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

    public void OnPointerClick(PointerEventData eventData) //?I?????
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
        if (sceneName == "NPC" && MapUnlock.isMapUnlock)
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
            // ?T?O?o???O GameManager ??b??????A?]???O Map ?????????? UI
            // ?p?G?A?? UI ???? DontDestroyOnLoad?A?o??N??????
            Destroy(ui);
        }

        if (ishover)
        {
            // ??j
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale * hoverscale,
                Time.deltaTime * hoverspeed
            );
        }
        else
        {
            // ???
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
