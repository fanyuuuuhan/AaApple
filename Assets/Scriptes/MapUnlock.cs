using UnityEngine;
using UnityEngine.UI;

public class MapUnlock : MonoBehaviour
{

    public float needStar = 0;
    public static bool isMapUnlock = false;
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.color = isMapUnlock ? Color.white : Color.gray;
    }

    void Update()
    {
        if (GameStar.AllStar >= needStar)
        {
            isMapUnlock = true;
        }
        else
        {
            isMapUnlock = false;
        }

        //根據狀態即時更新顏色
        if (img != null)
        {
            img.color = isMapUnlock ? Color.white : Color.gray;
        }

    }
}
