using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueLoader;
using static DialogueItem;
using UnityEngine.SceneManagement;

public class StoryLoader : MonoBehaviour
{ 
    
    public string Player;
    public TextAsset inkJSON;
    public Story story;
    public TextMeshProUGUI dialogueBox;
    public TextMeshProUGUI stateBox;
    public GameObject dialogueChoices;
    public DialogueLoader dialogueOptionManager;
    public GameObject dialogueCanvas;
    public GameSaveLoader sceneLoader;
    //characters
    public GameObject steveSprite;
    public GameObject rivalSprite;
    public GameObject senpaiSprite;
    public GameObject prof_henrySprite;
    //backgrounds
    public GameObject locker;
    public GameObject locker2;
    public GameObject lib;
    public GameObject grounds;
    public GameObject classroom;
    public GameObject caf;
    public GameObject caf2;
    //SaveData
    public GameObject myGameObject;
    public GAMEMYDATA SaveHolder;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            myGameObject = GameObject.Find("GAMEMYDATA");
            SaveHolder = (GAMEMYDATA)myGameObject.GetComponent(typeof(GAMEMYDATA));
            Player = SaveHolder.mySave.name;
            Debug.Log("Success");
        }
        catch
        {
            Player = "You";
        }
        story = new(inkJSON.text);
        dialogueChoices.SetActive(false);
        story.variablesState["Player"] = Player;
        this.LoadProgress();
        this.nextStoryLine();
    }

    public void LoadProgress()
    {
        string savedProgress = SaveHolder.mySave.storyProgress;
        if (!string.IsNullOrEmpty(savedProgress))
        {
            story.state.LoadJson(savedProgress);
        }
    }

    public void SaveProgress()
    {
        SaveHolder.mySave.storyProgress = story.state.ToJson();
        SaveHolder.saveData();
    }

    private void OnEnable()
    {
        dialogueSub += madeDialogueChoice;

    }


    private void OnDisable()
    {
        dialogueSub -= madeDialogueChoice;
    }

    public void madeDialogueChoice(string name)
    {
        Debug.Log(int.Parse(name));
        story.ChooseChoiceIndex(int.Parse(name));
        this.nextStoryLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void skipButton()
    {
        while (this.nextStoryLine())
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
                    return Player;
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

    public void nextButton()
    {
        this.nextStoryLine();
    }


    public bool nextStoryLine()
    {
        this.SaveProgress();
        if (done)
        {
            SceneManager.LoadScene(0);
            Destroy(myGameObject);
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
                rivalSprite.SetActive(speaker == "Rival");
                prof_henrySprite.SetActive(speaker == "Professor" || speaker == "Professor Harry");
                christofferSprite.SetActive(speaker == "Christoffer");
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