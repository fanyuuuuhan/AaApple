using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardFlip : MonoBehaviour
{
    public Sprite frontSprite;   // 正面
    public Sprite backSprite;    // 背面
    public float flipDuration = 0.4f;

    private Image img;
    private bool isFront = false;
    private bool isFlipping = false;

    //解鎖卡片
    public bool isUnlock = false;
    public string cardKey; // 一樣的 Key



    void Start()
    {
        // 測試用：清除存檔

        //PlayerPrefs.DeleteKey(cardKey);

        //讀取是否解鎖
        isUnlock = PlayerPrefs.GetInt(cardKey, 0) == 1;

        img = GetComponent<Image>();

        // 一開始顯示背面
        img.sprite = backSprite;
        transform.localScale = Vector3.one;

        //未解鎖卡片呈現灰色
        img.color = isUnlock ? Color.white : Color.gray;
        Debug.Log($"[Map] {cardKey} isUnlock={isUnlock}");

        //並改變顏色
        UpdateLockVisual();
    }

    void UpdateLockVisual()
    {
        img.color = isUnlock ? Color.white : Color.gray;
    }

    // 給 Button OnClick 呼叫
    public void OnCardClicked()
    {
        if (!isUnlock) return;

        if (isFlipping || isFront) return; // 已翻或正在翻就不處理
        StartCoroutine(FlipToFront());
    }

    IEnumerator FlipToFront()
    {
        isFlipping = true;
        float half = flipDuration / 2f;

        // 縮小
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            float x = Mathf.Lerp(1, 0, t / half);
            transform.localScale = new Vector3(x, 1, 1);
            yield return null;
        }

        // 換成正面
        img.sprite = frontSprite;
        isFront = true;

        // 放大
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            float x = Mathf.Lerp(0, 1, t / half);
            transform.localScale = new Vector3(x, 1, 1);
            yield return null;
        }

        transform.localScale = Vector3.one;
        isFlipping = false;
    }


}
