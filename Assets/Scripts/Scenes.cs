using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scenes : MonoBehaviour
{
    public void Analyze()
    {
        SceneManager.LoadScene("Analysis", LoadSceneMode.Single);
    }
    public void Pause()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Single);
    }
    public void BacktoMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
