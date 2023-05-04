using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveFile;

public class GAMEMYDATA : MonoBehaviour
{
    public Save mySave;

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
        SaveFile.updateSave(mySave.playerID.ToString(), mySave);
    }
}
