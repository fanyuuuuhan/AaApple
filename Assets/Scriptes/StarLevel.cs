using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class StarLevel : MonoBehaviour
{
    public Timer Timer;
    public Image[] star;

    public float Level1 = 0;
    public float Level2 = 0;
    public float Level3 = 0;

    public static bool isStar = false;

    void Start()
    {

    }



    void Update()
    {
        if (Timer.StarShow)
        {
            Timer.StarShow = false;
            StartCoroutine(DelayAction());

        }
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(2f);  // µ¥ 3 ¬í

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

