using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader instance;
    public static LevelLoader Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else
            Destroy(gameObject);

        Time.timeScale = 1f;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;  
    }

    public void PauseScene()
    {
        if (Time.timeScale == 0f)
            return;

        else
            Time.timeScale = 0f;
    }

    public void ResumeScene()
    {
        Time.timeScale = 1f;
        PlayerController.GamePaused.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
