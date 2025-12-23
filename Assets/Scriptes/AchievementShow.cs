using UnityEngine;

public class AchievementShow : MonoBehaviour
{
    public GameObject achievement;


    public void Achievement()
    {
        achievement.SetActive(true);
    }
    public void Close()
    {
        achievement.gameObject.SetActive(false); 
    }
}
