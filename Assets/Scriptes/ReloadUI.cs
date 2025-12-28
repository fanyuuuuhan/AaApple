using UnityEngine;

public class ReloadUI : MonoBehaviour
{
    public GameObject NowUI;

    void Start()
    {
        GameManager.Instance.PlayerUIPrefab = NowUI;
        GameManager.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
