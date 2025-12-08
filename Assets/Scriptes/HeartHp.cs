using UnityEngine;
using UnityEngine.UI;

public class HeartHp : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    public void UpdateHearts(int HP, int maxHP)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartValue = (i + 1) * 2;

            if (HP >= heartValue)
            {
                hearts[i].sprite = fullHeart; // 2 HP
            }
            else if (HP == heartValue - 1)
            {
                hearts[i].sprite = halfHeart; // 1 HP
            }
            else
            {
                hearts[i].sprite = emptyHeart; // 0 HP
            }
        }
    }


}
