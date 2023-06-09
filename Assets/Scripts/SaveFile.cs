using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Mono.Data.Sqlite;
using TMPro;
using System;
using static PlayerDB;



public class SaveFile : MonoBehaviour
{

    public PlayerDB db;

    public delegate void newSAVEFILE(string msg);
    public static event newSAVEFILE newSAVEFILEAnnounce;

    [SerializeField] public TMP_InputField inputName;
    public TMP_InputField saveFileName;

    public bool isMale;



    [System.Serializable]

    public class Save
    {
        public string name;
        public bool male;
        public int scene;
        public int chapter;
        public long playerID;
        public string[] items = new string[14];
        public string storyProgress;
        public string lastSpeaker = "";
        // public InventoryItem[] items = new InventoryItem[14];
        public string fileName;
    }

    public Save currentPlayer;

    public Save newSave = new();

    public void createSave()
    {
        //collect data from input
        Save newSave = new();
        newSave.name = inputName.text;
        newSave.scene = 1;
        newSave.male = isMale;
        newSave.chapter = 1;
        for (int i = 0; i < newSave.items.Length; i++) {
            newSave.items[i] = "-1_0";
        }
        db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>(); //dbTag is assigned to Database object in Unity
        db.CreateDB();
        db.addUser(newSave.name);
        newSave.playerID = db.getLatestPlayerID();
        newSave.chapter = 1;
        newSave.scene = 1;
        newSave.fileName = newSave.playerID.ToString();

        string json = JsonUtility.ToJson(newSave, true);
        File.WriteAllText(Application.dataPath + "/Saves/" + newSave.fileName + ".json",json);
        newSAVEFILEAnnounce(newSave.playerID.ToString());
    }

    public static void editSaveName(string name, string newName)
    {
        //db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>();
        string fileName = name;
        string directory = Application.dataPath + "/Saves/" + fileName + ".json";
        if (File.Exists(directory))
        {
            Debug.Log("Change Name");
            string json = File.ReadAllText(Application.dataPath + "/Saves/" + fileName + ".json");
            Save data = JsonUtility.FromJson<Save>(json);
            data.name = newName;
            string jsonToSave = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.dataPath + "/Saves/" + fileName + ".json",jsonToSave);
            editName(long.Parse(fileName), data.name);
            //db.editName(long.Parse(fileName),data.name);
        }   
        else
        {
            Debug.Log("File does not exist");
            return;
        }
    }

    public static void Delete(string name)
    {
        //db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>();
        string fileName = name;
        string directory = Application.dataPath + "/Saves/" + fileName + ".json";
        if (File.Exists(directory))
        {
            File.Delete(Application.dataPath + "/Saves/" + fileName + ".json"); 
        }
        else
        {
            Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + directory);
        }
        
    } 

    public static Save loadSave (string name)
    {
        string filename = name;
        string json = File.ReadAllText(Application.dataPath + "/Saves/" + filename + ".json");
        Save data = JsonUtility.FromJson<Save>(json);
        return data;
    }

    public static void updateSave(string name, Save updatedSave)
    {
        //db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>();
        string fileName = name;
        string directory = Application.dataPath + "/Saves/" + fileName + ".json";
        if (File.Exists(directory))
        {
            string json = File.ReadAllText(Application.dataPath + "/Saves/" + fileName + ".json");
            Save data = JsonUtility.FromJson<Save>(json);
            string jsonToSave = JsonUtility.ToJson(updatedSave, true);
            File.WriteAllText(Application.dataPath + "/Saves/" + fileName + ".json", jsonToSave);
            insLastAccess(long.Parse(name));
        }
        else
        {
            Debug.Log("File does not exist");
            return;
        }
    }
    
}
