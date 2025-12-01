using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckInit : MonoBehaviour
{
    public static string debugScene;

    public static int startpointNumber;
    public GameObject Player;

     void Start()
    {
        
        //||!GameObject.Find("Canvas/Image-HP/HPber")GetComponent<Image>
        if (!GameObject.Find("Player"))
        {
            SceneManager.LoadScene("Init");
            debugScene = SceneManager.GetActiveScene().name;
        }
        if (startpointNumber != 0)
        {
            GameObject g = GameObject.Find(startpointNumber.ToString()) as GameObject;
            if (g != null)
            {
                Player.transform.position = g.transform.position;
            }
            startpointNumber = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
