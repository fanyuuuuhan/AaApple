using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementBTN : MonoBehaviour
{
    public Sprite Ach_0;
    public Sprite Ach_back;

    bool ishover = false;
    private Image img;

    void Start()
    {
        // 初始化獲取 Image 組件
        img = GetComponent<Image>();
        img.sprite = Ach_0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ishover = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ishover = false;
    }


    void Update()
    {
        if(ishover)
        {
            img.sprite = Ach_back; 

        }
        else
        {
            img.sprite = Ach_0;
        }
    }
}
