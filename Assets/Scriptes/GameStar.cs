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


    // ???a????????J?A?o?????X?{??N???????
    void Start()
    {
        AllStar = PlayerData.AllOfStar;
        UpdateStarDisplay();
    }

    public void UpdateStarDisplay()
    {
        // ????£TG?M???s??
        //PlayerPrefs.DeleteKey(Gamekey);

        // ?q?s??????P?? (?w?]?? 0)
        int stars = PlayerPrefs.GetInt(Gamekey, 0);

        // ?????????????
        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);

        // ???P?????????????
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
                // 0 ???P????????????A?£YA?i?H?[?@?i?u???L???v?????
                break;
        }
    }

    void Update()
    {
        PlayerData.AllOfStar = AllStar;
        // StarNum.text = $"{AllStar:f0}";
    }
}
