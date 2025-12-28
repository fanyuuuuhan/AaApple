using UnityEngine;
using UnityEngine.EventSystems;

public class HoverRotate : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    bool ishover = false;
    Quaternion originRotate;
    Quaternion target;
    public float angel = 0;
    float speed = 5f;

    void Start()
    {
        originRotate= transform.localRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ishover = true;
        target = Quaternion.Euler(0, 0, angel);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ishover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ishover)
        {
            transform.rotation=Quaternion.Lerp(transform.rotation, target, speed* Time.unscaledDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originRotate, speed* Time.unscaledDeltaTime);
        }
    }
}
