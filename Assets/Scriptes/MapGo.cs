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
    }

    public void OnPointerClick(PointerEventData eventData) //點擊事件
    {
        PlayerData.NextSceneName = sceneName;

        SceneManager.LoadScene("Init");
    }

    void Update()
    {
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
