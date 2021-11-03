using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Scenes : MonoBehaviour
{
    public void Start()
    {

    }
    public void Analyze()
    {
        SceneManager.LoadScene("Analysis", LoadSceneMode.Single);
    }
    public void Pause()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Single);
    }
    public void Resume()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
