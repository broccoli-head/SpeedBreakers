using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public ScrollingBackground background;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PlayAgain()
    {
        HealthManager.playerCount = 2;
        ScrollingBackground.gameRunning = true;
        background.speed = 2f;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
