using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveFile;

public class GAMEMYDATA : MonoBehaviour
{
    public Save mySave;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HomeScreenScene")
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        newSAVEFILEAnnounce += assignData;
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        newSAVEFILEAnnounce -= assignData;
    }


    public void assignData(string name)
    {
        mySave = loadSave(name);
        Debug.Log(name + " loaded success");
    }

    public void clearData()
    {
        mySave = new();
    }

    public void saveData()
    {
        updateSave(mySave.fileName.ToString(), mySave);
    }

    public void buttonloadGame()
    {
        SceneManager.LoadScene("TestGameplay");
    }
}
