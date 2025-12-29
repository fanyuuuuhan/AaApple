using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScale : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{

    //hover放大
    float hoverspeed = 8f;
    public float hoverscale = 1.5f;
    bool ishover = false;
    Vector3 originalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (ishover)
        {
            // 放大
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale * hoverscale,
                Time.unscaledDeltaTime * hoverspeed
            );
        }
        else
        {
            // 還原
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale,
                Time.unscaledDeltaTime * hoverspeed
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
