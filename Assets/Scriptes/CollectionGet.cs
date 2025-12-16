using UnityEngine;
using UnityEngine.UI;

public class CollectionGet : MonoBehaviour
{
    public Image[] Collection;

    void Start()
    {
        // 初始化全部為灰色
        foreach (Image img in Collection)
        {
            img.color = Color.gray;
        }
    }

    public void ActivateItem(int id)
    {
        if (id >= 0 && id < Collection.Length)
        {
            Collection[id].color = Color.white; // 顯示彩色
        }
    }
}
