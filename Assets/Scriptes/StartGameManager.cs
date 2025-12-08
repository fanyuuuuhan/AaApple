using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Map");
    }
}
