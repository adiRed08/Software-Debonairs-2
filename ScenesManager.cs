using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene 
    {
        HomeScreenScene,
        NewGameScene,
        LoadGameScene
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.NewGameScene.ToString());
    }

    public void LoadLoadScene()
    {
        SceneManager.LoadScene(Scene.LoadGameScene.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.HomeScreenScene.ToString());
        
    }

}
