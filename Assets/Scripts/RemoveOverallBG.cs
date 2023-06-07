using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveOverallBG : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleStageScene")
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
