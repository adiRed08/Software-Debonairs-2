using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerDB;
using static LoadSaveItem;
using UnityEngine.SceneManagement;

public class GameSaveLoader : MonoBehaviour
{
    public long toLoad;
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject prefab;

    public long playerID;
    public Button Next;

    public GameObject theGAMEMYDATAobject;


    private void Start()
    {
        Dictionary<long, string> saves = getDictSaves();

        DontDestroyOnLoad(theGAMEMYDATAobject);

        toLoad = 0;

        foreach (KeyValuePair<long, string> keyValues in saves)
        {
            GameObject gameObject = Instantiate(prefab, scrollViewContent);
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Save #" + toLoad++;
            gameObject.name = keyValues.Key.ToString();
            TextMeshProUGUI[] texts = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
            texts[1].text = keyValues.Value.Trim();
        }
    }

    public void OnEnable()
    {
        clickSaveItemAnnounce += setLoadSave;
    }

    public void OnDisable()
    {
        clickSaveItemAnnounce -= setLoadSave;
    }

    public void RemoveSave(string name)
    {
        int count = scrollViewContent.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = scrollViewContent.GetChild(i);
            if(child.name == name)
            {
                Destroy(child);
                Debug.Log("Found Thing");
            }
        }
    }

    public void setLoadSave(string name)
    {
        playerID = long.Parse(name);
        if(name == "0")
        {
            Next.interactable = false;
        }
        else
        {
            Next.interactable = true;
        }
    }

    public void buttonLoadSave()
    {
        if (playerID != 0) {
            Debug.Log(getPlayerName(playerID));
            GAMEMYDATA script = theGAMEMYDATAobject.GetComponent<GAMEMYDATA>();
            script.assignData(playerID.ToString());
            SceneManager.LoadScene("TestGameplay");

        }

    }

    public void loadBattle()
    {
        SceneManager.LoadScene("BattleStageScene");
    }
}
