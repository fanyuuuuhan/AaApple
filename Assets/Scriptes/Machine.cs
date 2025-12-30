using System.Collections;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public int id;
    public GameObject fixCanva;
    public GameObject readyCanva;
    public GameObject progressBar;
    public float fixTime = 5f;   // 修理總時間
    public int status = -1;    //-1還缺材料，0準備好了，1正在修（運轉）
    CollectionGet_Butter cgb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cgb = GameManager.Instance.PlayerUI.GetComponentInChildren<CollectionGet_Butter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cgb.Collection.Contains(0) && id == 1 && status == -1)
        {
            status = 0;
        }
        else if (cgb.Collection.Contains(5) && id == 2 && status == -3)
        {
            status = -2;
        }
        else if (cgb.Collection.Contains(1) && id == 2 && status == -1)
        {
            status = 0;
        }
        else if (cgb.Collection.Contains(2) && id == 3 && status == -1)
        {
            status = 0;
        }
        else if (cgb.Collection.Contains(3) && id == 4 && status == -1)
        {
            status = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (status)
            {
                case 0:
                case -1:
                    readyCanva.SetActive(true);
                    fixCanva.SetActive(false);
                    StartCoroutine(FadeInHint());
                    break;
                default:
                    fixCanva.SetActive(false);
                    readyCanva.SetActive(false);
                    StartCoroutine(FadeInHint());
                    break;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (status)
            {
                case 0:
                case -1:
                    readyCanva.SetActive(true);
                    fixCanva.SetActive(false);
                    break;
                default:
                    fixCanva.SetActive(false);
                    readyCanva.SetActive(false);
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(FadeOutHint());
        }
    }

    public IEnumerator FixMachine()
    {
        if (id == 1)
        {
                status = 1;
                StartCoroutine(FadeOutHint());
                progressBar.SetActive(true);
                GameObject barFill = progressBar.transform.GetChild(2).gameObject;
                float currentFix = 0f; // 當前修理進度 0→fixTime
                Vector3 startScale = barFill.transform.localScale;
                Vector3 endScale = new Vector3(1f, startScale.y, startScale.z);
                while (currentFix < fixTime)
                {
                    currentFix += Time.deltaTime;

                    // 直接用比例算 X 軸
                    float ratio = Mathf.Clamp01(currentFix / fixTime);
                    barFill.transform.localScale = new Vector3(ratio, barFill.transform.localScale.y, barFill.transform.localScale.z);
                    yield return null;
                }
                // 確保最後填滿
                barFill.transform.localScale = endScale;
                progressBar.SetActive(false);
                barFill.transform.localScale = new Vector3(0, barFill.transform.localScale.y, barFill.transform.localScale.z);
                // status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().isMelted ? -1 : 2;
        }
        else if (id == 2)
        {
            if (status == -2)
            {
                cgb.Collection.Remove(5);
                //gear active
                //rotate
                status = -1;
            }
            else if (status == 0)
            {
                status = 1;
                StartCoroutine(FadeOutHint());
                progressBar.SetActive(true);
                GameObject barFill = progressBar.transform.GetChild(2).gameObject;
                float currentFix = 0f; // 當前修理進度 0→fixTime
                Vector3 startScale = barFill.transform.localScale;
                Vector3 endScale = new Vector3(1f, startScale.y, startScale.z);
                while (currentFix < fixTime)
                {
                    currentFix += Time.deltaTime;

                    // 直接用比例算 X 軸
                    float ratio = Mathf.Clamp01(currentFix / fixTime);
                    barFill.transform.localScale = new Vector3(ratio, barFill.transform.localScale.y, barFill.transform.localScale.z);
                    yield return null;
                }
                // 確保最後填滿
                barFill.transform.localScale = endScale;
                progressBar.SetActive(false);
                barFill.transform.localScale = new Vector3(0, barFill.transform.localScale.y, barFill.transform.localScale.z);
            }
        }
    }

    public IEnumerator FadeInHint()
    {
        for (float t = 0; t <= 1 ; t+=0.02f)
        {
            gameObject.GetComponentInChildren<CanvasGroup>().alpha = t;
            yield return null;
        }
    }
    public IEnumerator FadeOutHint()
    {
        for (float t = 1; t >= 0 ; t-=0.02f)
        {
            gameObject.GetComponentInChildren<CanvasGroup>().alpha = t;
            yield return null;
        }
        fixCanva.SetActive(false);
        readyCanva.SetActive(false);
    }
}
