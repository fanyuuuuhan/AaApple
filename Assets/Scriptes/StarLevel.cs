using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StarLevel : MonoBehaviour
{
    public Timer Timer;
    public Image[] star;

    //UI²¾°Êªk
    RectTransform rect;
    public float speed = 50;
    bool StarMove = false;

    public float Level1 = 0;
    public float Level2 = 0;
    public float Level3 = 0;

    public static bool isStar = false;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }



    void Update()
    {
        if (Timer.StarShow)
        {
            Timer.StarShow = false;
            StarMove = true;
            
        }
        if (StarMove)
        {
            if (rect.anchoredPosition.y > 0)
            {
                rect.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);
            }
            else if (rect.anchoredPosition.y < 0)
            {
                rect.anchoredPosition = Vector2.zero;
                StarMove = false;
                StartCoroutine(DelayAction());
            }
        }
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(1f);  // µ¥ 1 ¬í

        if (Timer.nowTimeM < Level1)
        {
            star[0].gameObject.SetActive(true);
            star[1].gameObject.SetActive(true);
            star[2].gameObject.SetActive(true);
            isStar = true;
        }
        else if (Timer.nowTimeM < Level2)
        {
            star[0].gameObject.SetActive(true);
            star[1].gameObject.SetActive(true);
            isStar = true;
        }
        else if (Timer.nowTimeM < Level3)
        {
            star[0].gameObject.SetActive(true);
            isStar = true;
        }
        else
        {
            isStar = false;
        }
    }
}

