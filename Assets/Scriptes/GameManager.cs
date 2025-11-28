using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image ESC;
    public Image setting;

    public static bool isRestart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ESC.gameObject.SetActive(false);
        setting.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESC.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ContiuneGame()
    {
        ESC.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        isRestart=true;
        ESC.gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameApple");


    }

    public void Setting()
    {
        setting.gameObject.SetActive(true);

    }

    public void BackMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
    }
}
