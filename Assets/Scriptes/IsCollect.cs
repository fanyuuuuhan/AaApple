using UnityEngine;

public class IsCollect : MonoBehaviour
{
    public string cardKey; // ¨Ò¦p "Card_Sword"

    public void Collect()
    {
        PlayerPrefs.SetInt(cardKey, 1);
        PlayerPrefs.Save();
    }
}
