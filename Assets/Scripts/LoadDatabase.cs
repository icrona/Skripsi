using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Data;
using Mono.Data.Sqlite;
public class LoadDatabase : MonoBehaviour {

    // Use this for initialization
    private string connectionString;
    private string filepath;
    private string localVersion;
    private string serverVersion;

    void Start () {
        PlayerPrefs.SetString("DeviceID",SystemInfo.deviceUniqueIdentifier);
        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
        if (!File.Exists(filepath))
        {
            PlayerPrefs.SetInt("UserData", 0);
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = String.Format("CREATE TABLE main.Cake (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE , Name TEXT NOT NULL , NumOfTier INTEGER NOT NULL, Size1 TEXT NOT NULL, Size2 TEXT, Size3 TEXT, Frosting TEXT NOT NULL, Flavour1 TEXT NOT NULL, Flavour2 TEXT, Flavour3 TEXT,Price TEXT NOT NULL, Image1 BLOB, Image2 BLOB, Image3 BLOB, Image4 BLOB, SavedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)");
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();

                    string query = String.Format("CREATE TABLE main.Version (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Version TEXT NOT NULL)");
                    dbCmd.CommandText = query;
                    dbCmd.ExecuteScalar();

                    string defaultVersion = String.Format("INSERT INTO main.Version (Version) VALUES ('default')");
                    dbCmd.CommandText = defaultVersion;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }

            //default player prefs
            PlayerPrefs.SetInt("NumOfSize", 3);
            PlayerPrefs.SetInt("NumOfFlavour", 4);

            PlayerPrefs.SetInt("Size0", 16);
            PlayerPrefs.SetInt("Size1", 20);
            PlayerPrefs.SetInt("Size2", 22);

            PlayerPrefs.SetInt("Shape0", 1);
            PlayerPrefs.SetInt("Shape1", 0);
            PlayerPrefs.SetInt("Shape2", 1);

            PlayerPrefs.SetString("Flavour0", "Vanila");
            PlayerPrefs.SetString("Flavour1", "Chocolate");
            PlayerPrefs.SetString("Flavour2", "Cheese");
            PlayerPrefs.SetString("Flavour3", "Red Velvet");

            PlayerPrefs.SetInt("Frosting0", 1);
            PlayerPrefs.SetInt("Frosting1", 1);
            PlayerPrefs.SetInt("Frosting2", 0);
        }

        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string getVersion = String.Format("SELECT Version FROM Version");
                dbCmd.CommandText = getVersion;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        localVersion = reader.GetString(0);
                    }
                }
                dbConnection.Close();
            }
        }

        //wwwform to get server version
        //get json config file from server

        //check if server version is equal phone version
        //if server version != local version set playerprefs from json config

        PlayerPrefs.SetInt("NumOfSize", 3);
        PlayerPrefs.SetInt("NumOfFlavour", 4);
        /*
        int test;
        for(int i = 0; i < 1; i++)
        {
            PlayerPrefs.SetInt("Size"+i, 16);
            test = PlayerPrefs.GetInt("Size" + i);
            Debug.Log(test);
        }
        */

        PlayerPrefs.SetInt("Size0", 16);
        PlayerPrefs.SetInt("Size1", 20);
        PlayerPrefs.SetInt("Size2", 22);

        PlayerPrefs.SetInt("Shape0", 1);
        PlayerPrefs.SetInt("Shape1", 0);
        PlayerPrefs.SetInt("Shape2", 1);

        PlayerPrefs.SetString("Flavour0", "Vanila");
        PlayerPrefs.SetString("Flavour1", "Chocolate");
        PlayerPrefs.SetString("Flavour2", "Cheese");
        PlayerPrefs.SetString("Flavour3", "Red Velvet");

        PlayerPrefs.SetInt("Frosting0", 1);
        PlayerPrefs.SetInt("Frosting1", 1);
        PlayerPrefs.SetInt("Frosting2", 0);

    }
	
}
