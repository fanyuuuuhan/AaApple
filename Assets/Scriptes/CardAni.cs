using UnityEngine;
using UnityEngine.UI;

public class CardAni : MonoBehaviour
{
    public Transform achievementList; // 拖入你的 AchievementList 物件
    Animator ani;

    private void Start()
    {
        ani= GetComponent<Animator>();
    }

    // 這個方法會在動畫結束時觸發
    public void JoinToLayout()
    {

        // 1. 先禁用 Animator，釋放對 Transform 的控制權
        if (ani != null)
        {
            ani.enabled = false;
        }


        // 1. 將父物件設為 ScrollView 的 Content
        transform.SetParent(achievementList);

        // 2. 歸零座標，讓 Layout Group 接手自動排版
        // 這樣它就會瞬間彈回原本該在的格子裡
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;

        // 3. 立即刷新 UI (避免殘影或跳格)
        LayoutRebuilder.ForceRebuildLayoutImmediate(achievementList as RectTransform);
    }
}