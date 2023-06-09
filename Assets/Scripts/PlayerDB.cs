using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using System.Collections.Generic;

public class PlayerDB : MonoBehaviour
{

    private static string db = "URI=file:savesInfo.db";
    private long currentPlayerID = 0;
    private Dictionary<long, string> xxx;

    // Start is called before the first frame update
    // void Start()
    // {
    //     CreateDB();
    //     addUser("adi");
    //     // editName(1678169846,"matt");
    //     // deleteUser(1678169846);
    //     System.Threading.Thread.Sleep(10000);
    //     setChapterID(currentPlayerID, 4);
    //     setSceneID(currentPlayerID, 20);

    //     Debug.Log("PlayerName: " + getPlayerName(currentPlayerID));
    //     Debug.Log("Chapter: " + getChapterID(currentPlayerID).ToString());
    //     Debug.Log("Scene: " + getSceneID(currentPlayerID).ToString());

    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    //Creates a table if it doesn't exist
    public void CreateDB()
    {
        //Creates database connection
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Persons(playerID INTEGER PRIMARY KEY, name TEXT,sceneID INTEGER, chapterID INTEGER, lastAccess INTEGER);";
                command.ExecuteNonQuery();
            }
            connection.Close();
            
        }
    }

    public void addUser(string name)
    {
        using (var connection = new SqliteConnection(db))
        {
        var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                setCurrentPlayerID(unixTimestamp);
                command.CommandText = "INSERT INTO Persons(name, playerID) VALUES (' " + name + " ', " + unixTimestamp.ToString() + ");";
                command.ExecuteNonQuery();
                insLastAccess(unixTimestamp);


            }
            connection.Close();
            setChapterID(unixTimestamp, 1);
            setSceneID(unixTimestamp, 1);
        }

    }

    public static void editName(long playerID, string newName){
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Persons SET name = ' "+newName+" ' WHERE playerID = "+ playerID + ";";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        insLastAccess(playerID);
    }

    public static void deleteUser(long playerID)
    {
        {
            using (SqliteConnection con = new SqliteConnection(db))
            {
                con.Open();
                using (SqliteCommand command = new SqliteCommand("DELETE FROM Persons WHERE playerID = "+ playerID +" ;",con))
                    {
                        command.ExecuteNonQuery();
                    }
                con.Close();
            }
            
        }
}

    //Gets the time last accessed of a save file
    public static void insLastAccess(long playerID)
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                command.CommandText = "UPDATE Persons SET lastAccess = " + unixTimestamp.ToString() + " WHERE playerID =  "+ playerID.ToString() +" ;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        } 
    }

    public static void setSceneID(long playerID, int sceneID)
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Persons SET sceneID = " + sceneID.ToString() + " WHERE playerID =  "+ playerID.ToString() +" ;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        } 
        insLastAccess(playerID);
    }

    public static void setChapterID(long playerID, int chapterID)
    {
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Persons SET chapterID = " + chapterID.ToString() + " WHERE playerID =  "+ playerID.ToString() +" ;";
                command.ExecuteNonQuery();
            }
            connection.Close();
        } 
        insLastAccess(playerID);
    }

    public void setCurrentPlayerID(long selectedPlayerID){
        currentPlayerID = selectedPlayerID;
    }

    public static int getChapterID(long playerID)
    {
        int retChapter = 0;
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT chapterID FROM Persons WHERE playerID = " + playerID.ToString();
                retChapter = int.Parse(command.ExecuteScalar().ToString());
            }
            connection.Close();
        }
        return retChapter;
    }

     public static int getSceneID(long playerID)
    {
        int retScene = 0; 
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT sceneID FROM Persons WHERE playerID = " + playerID.ToString();
                retScene = int.Parse(command.ExecuteScalar().ToString());
            }
            connection.Close();
        }
        return retScene;
    }

    public static string getPlayerName(long playerID)
    {
        string retName = ""; 
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name FROM Persons WHERE playerID = " + playerID.ToString();
                retName = command.ExecuteScalar().ToString();
            }
            connection.Close();
        }
        return retName;
    }

    // this function is for convenience in creating a new save
    public long getLatestPlayerID()
    {
        long retID; 
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();
            //creates "command" which allows db control
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT MAX(ROWID) FROM Persons";
                retID = long.Parse(command.ExecuteScalar().ToString());
            }
            connection.Close();
        }
        return retID;
    }
    
    public static Dictionary<long, string> getDictSaves()
    //public void getDictSaves()

    {
        Dictionary<long, string> dictionary = new Dictionary<long, string>();
 
        using (var connection = new SqliteConnection(db))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT playerID, name FROM Persons";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dictionary.Add(reader.GetInt64(0), reader.GetString(1));
                    }
                }
                connection.Close();
            }
        }
        Debug.Log(DictionaryToString(dictionary));
        return dictionary;

    }

    private static string DictionaryToString(Dictionary<long, string> dictionary)
    {
        string dictionaryString = "{";
        foreach (KeyValuePair<long, string> keyValues in dictionary)
        {
            dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
        }
        return dictionaryString.TrimEnd(',', ' ') + "}";
    }

}

