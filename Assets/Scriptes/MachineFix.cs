using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MachineFix : MonoBehaviour
{

    public GameObject Fixtext;
    public GameObject FixOver;
    public bool isFixable = true; //可以修理
    public Image FixBar;

    //隱藏字
    public void HideText()
    {
        Fixtext.SetActive(false);
        FixOver.SetActive(false);
    }
    //顯示字
    public void ShowText()
    {
        if (isFixable)
        {
            Fixtext.SetActive(true);
        }
        
    }
    // 開始修理，文字消失，標記修好
    public void StartFix()
    {
        if (!isFixable) return;
        Fixtext.SetActive(false);
        isFixable = false;

    }

}
