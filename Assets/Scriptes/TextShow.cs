using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class TextShow : MonoBehaviour
{
    public float ShowTime = 2f;
    public float StopShowTime = 2f;

    public GameObject TextGo;

    IEnumerator ShowTimePause()
    {
        yield return new WaitForSeconds(ShowTime);
        TextGo.gameObject.SetActive(true);
        yield return new WaitForSeconds(StopShowTime);
        TextGo.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(ShowTimePause());
    }
}
