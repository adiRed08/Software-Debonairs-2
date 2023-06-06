using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueLoader;
using static DialogueItem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StoryLoader : MonoBehaviour
{ 
    [SerializeField] private string _player;
    [SerializeField] private TextAsset _inkJSON;
    private Story story;
    [SerializeField] private TextMeshProUGUI dialogueBox;
    [SerializeField] private TextMeshProUGUI stateBox;
    [SerializeField] private GameObject dialogueChoices;
    [SerializeField] private DialogueLoader dialogueOptionManager;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private GameSaveLoader sceneLoader;
    //characters
    [SerializeField] private GameObject steveSprite;
    [SerializeField] private GameObject rivalSprite;
    [SerializeField] private GameObject senpaiSprite;
    [SerializeField] private GameObject prof_henrySprite;
    //backgrounds
    [SerializeField] private GameObject locker;
    [SerializeField] private GameObject locker2;
    [SerializeField] private GameObject lib;
    [SerializeField] private GameObject grounds;
    [SerializeField] private GameObject classroom;
    [SerializeField] private GameObject caf;
    [SerializeField] private GameObject caf2;
    //SaveData
    [SerializeField] private GameObject _myGameObject;
    [SerializeField] private GAMEMYDATA _saveHolder;
    private bool done;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            //Try to find GAMEDATA
            _myGameObject = GameObject.Find("GAMEMYDATA");
            _saveHolder = (GAMEMYDATA)_myGameObject.GetComponent(typeof(GAMEMYDATA));
            _player = _saveHolder.mySave.name;
            Debug.Log("Success");
        }
        catch
        {
            //Default to You as the player name
            _player = "You";
        }
        //Load story
        story = new(_inkJSON.text);
        dialogueChoices.SetActive(false);
        story.variablesState["Player"] = _player;
        this.LoadProgress();
        this.NextStoryLine();
    }

    public void LoadProgress()
    {
        string savedProgress = _saveHolder.mySave.storyProgress;
        if (!string.IsNullOrEmpty(savedProgress))
        {
            story.state.LoadJson(savedProgress);
        }
    }

    public void SaveProgress()
    {
        _saveHolder.mySave.storyProgress = story.state.ToJson();
        _saveHolder.saveData();
    }

    private void OnEnable()
    {
        dialogueSub += MadeDialogueChoice;
    }

    private void OnDisable()
    {
        dialogueSub -= MadeDialogueChoice;
    }

    public void MadeDialogueChoice(string name)
    {
        Debug.Log(int.Parse(name));
        story.ChooseChoiceIndex(int.Parse(name));
        this.NextStoryLine();
    }

    public void SkipButton()
    {
        while (this.NextStoryLine())
        {
            Debug.Log("skip");
        }
    }

    private string GetState(List<string> tags)
    {
        foreach (string tag in tags)
        {
            //Debug.Log(tag);
            if (tag.StartsWith("battletrigger"))
            {
                sceneLoader.loadBattle();
            }
            else if (tag.StartsWith("background:"))
            {
                Debug.Log(tag.Substring("character:".Length));
                locker.SetActive(":locker"==tag.Substring("character:".Length));
                locker2.SetActive(":locker2"==tag.Substring("character:".Length));
                lib.SetActive(":lib"==tag.Substring("character:".Length));
                grounds.SetActive(":grounds"==tag.Substring("character:".Length));
                classroom.SetActive(":classroom"==tag.Substring("character:".Length));
                caf.SetActive(":caf"==tag.Substring("character:".Length));
                caf2.SetActive(":caf2"==tag.Substring("character:".Length));
            }
            if (tag.StartsWith("character:"))
            {
                if(tag.Substring("character:".Length) == "Player")
                {
                    return _player;
                }
                return tag.Substring("character:".Length).Trim();
            }
            else if (tag.StartsWith("thoughts"))
            {
                return "*player thoughts*";
            }
        }
        return null;
    }

    public void NextButton()
    {
        this.NextStoryLine();
    }


    public bool NextStoryLine()
    {
        this.SaveProgress();
        if (done)
        {
            SceneManager.LoadScene(0);
            Destroy(_myGameObject);
            return false;
        }
        List<string> listChoices = new();
        if (story.canContinue)
        {
            dialogueChoices.SetActive(false);
            dialogueCanvas.SetActive(true);
            
            dialogueBox.text = story.Continue();
            // Get the current speaker variable from the VariablesState dictionary
            string speaker = GetState(story.currentTags);

            // Display the speaker and line in your game
            Debug.Log(speaker);
            stateBox.text = speaker;
            steveSprite.SetActive(speaker == "Steve" || speaker == "Mysterious Guy");
            prof_henrySprite.SetActive(speaker == "Professor" || speaker == "Professor Harry");
            rivalSprite.SetActive(speaker == "Christoffer" || speaker == "Rival");
            senpaiSprite.SetActive(speaker == "Justine");
            return true;
        }
        else
        {
            if (story.currentChoices.Count == 0)
            {
                story.ChoosePathString("TBC");
                this.SaveProgress();
                dialogueChoices.SetActive(false);
                dialogueCanvas.SetActive(true);

                dialogueBox.text = story.Continue();
                // Get the current speaker variable from the VariablesState dictionary
                string speaker = GetState(story.currentTags);

                // Display the speaker and line in your game
                Debug.Log(speaker);
                stateBox.text = speaker;
                steveSprite.SetActive(speaker == "Rival");
                prof_henrySprite.SetActive(speaker == "Professor" || speaker == "Professor Harry");
                rivalSprite.SetActive(speaker == "Christoffer" || speaker == "Rival");
                senpaiSprite.SetActive(speaker == "Justine");
                done = true;
                return false;
            }
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                listChoices.Add(story.currentChoices[i].text);
            } 
            dialogueOptionManager.SetChoices(listChoices);
            dialogueOptionManager.RefreshChoices();
            dialogueChoices.SetActive(true);
            dialogueCanvas.SetActive(false);
            return false;
        }
    }
}