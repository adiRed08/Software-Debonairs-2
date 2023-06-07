using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveFile;

public class SetDataOnScene : MonoBehaviour
{    
    //SaveData
    public GameObject myGameObject;
    public GAMEMYDATA SaveHolder;
    public GameObject EditNameInputPanel;
    public Save MySave;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Chapter;
    public TextMeshProUGUI Scene;
    public TextMeshProUGUI PlayerID;


    public TMP_InputField NameInput;
    public TMP_InputField ChapterInput;
    public TMP_InputField SceneInput;


    // Start is called before the first frame update
    void Start()
    {
        // Find persistent GAMEMYDATA
        try
        {
            myGameObject = GameObject.Find("GAMEMYDATA");
            SaveHolder = (GAMEMYDATA)myGameObject.GetComponent(typeof(GAMEMYDATA));
            Debug.Log("Success");
            MySave = SaveHolder.mySave;
        }
        catch
        {
            Debug.Log("Persistent GAMEMYDATA not found");
        }
        // Get needed data from GAMEMYDATA
        try
        {
            Name.text = MySave.name;
            NameInput.text = MySave.name;
            PlayerID.text = MySave.playerID.ToString();
            Chapter.text = MySave.chapter.ToString();
            ChapterInput.text = MySave.chapter.ToString();
            Scene.text = MySave.scene.ToString();
            SceneInput.text = MySave.scene.ToString();
        }
        catch
        {
            Debug.Log("Could not retrieve data from GAMEMYDATA");
        }
    }

    public void updateName(string NewName)
    {
            Name.text = NewName;
            NameInput.text = NewName;
            MySave.name = NewName;
    }

    public void editNameButton()
    {
        if (EditNameInputPanel.activeInHierarchy)
        {
            EditNameInputPanel.SetActive(false);
        }
        else
        {
            NameInput.text = MySave.name;
            EditNameInputPanel.SetActive(true);
        }
    }

    public void saveButtonName()
    {
        Name.text = NameInput.text;
        MySave.name = NameInput.text;
    }


    public void updateChapter(string NewChapter)
    {
        int resultInt;
        if (int.TryParse(NewChapter,out resultInt))
        {
            Chapter.text = NewChapter;
            ChapterInput.text = NewChapter;
            MySave.chapter = resultInt;
            
        }
        else
        {
            Debug.Log("Not a proper Chapter Number");
        }
    }

    public void updateScene(string NewScene)
    {
        int resultInt;
        if (int.TryParse(NewScene, out resultInt))
        {
            Scene.text = NewScene;
            SceneInput.text = NewScene;
            MySave.scene = resultInt;
        }
        else
        {
            Debug.Log("Not a proper Scene Number");
        }
    }

    public void updateSave()
    {
        SaveHolder.saveData();
        PlayerDB.editName(MySave.playerID, MySave.name);
        PlayerDB.setSceneID(MySave.playerID,MySave.scene);
        PlayerDB.setChapterID(MySave.playerID, MySave.chapter);
    }

    public void ResumeButton()
    {
        SceneManager.LoadScene("Scene_" + MySave.scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
