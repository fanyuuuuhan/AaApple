using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameStar : MonoBehaviour
{
    public string Gamekey;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public static float AllStar = 0;

    public  TextMeshProUGUI StarNum;


    // 當地圖場景載入，這個物件出現時就執行顯示
    void Start()
    {
        AllStar = PlayerData.AllOfStar;
        UpdateStarDisplay();
    }

    public void UpdateStarDisplay()
    {
        // 測試用：清除存檔
        PlayerPrefs.DeleteKey(Gamekey);

        // 從存檔讀取星數 (預設為 0)
        int stars = PlayerPrefs.GetInt(Gamekey, 0);

        // 先把所有圖片隱藏
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);

        // 根據星數顯示對應的圖片
        switch (stars)
        {
            case 1:
                Star1.SetActive(true);
                AllStar = AllStar + 1;
                break;
            case 2:
                Star2.SetActive(true);
                AllStar = AllStar + 2;
                break;
            case 3:
                Star3.SetActive(true);
                AllStar = AllStar + 3;
                break;
            default:
                // 0 顆星時不顯示任何圖片，或你可以加一張「未過關」的灰圖
                break;
        }
    }

    void Update()
    {
        PlayerData.AllOfStar = AllStar;
        StarNum.text = $"{AllStar:f0}";
    }
}
