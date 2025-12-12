using UnityEngine;
using UnityEngine.UI;

public class StarLight : MonoBehaviour
{
    public Image star;
    Vector3 originalScale;
    public float Changespeed = 8f;
    public float Changescale = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (StarLevel.isStar)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale * Changescale,
                Time.deltaTime * Changespeed
            );
        }
    }
}
