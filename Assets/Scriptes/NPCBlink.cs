using UnityEngine;

public class NPCBlink : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    public float switchTime = 0.4f;

    SpriteRenderer sr;
    float timer;
    bool useFirst = true;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite1;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchTime)
        {
            timer = 0f;
            useFirst = !useFirst;
            sr.sprite = useFirst ? sprite1 : sprite2;
        }
    }
}
