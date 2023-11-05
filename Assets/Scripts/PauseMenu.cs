using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static bool GameIsPaused = false;

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PauseGame()
    {
        if (GameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            Pause();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
