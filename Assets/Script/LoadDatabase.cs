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

    void Start () {
        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
        if (!File.Exists(filepath))
        {
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = String.Format("CREATE TABLE main.Cake (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE , Name TEXT NOT NULL , Size TEXT NOT NULL , Flavour TEXT NOT NULL , Image1 BLOB, Image2 BLOB, Image3 BLOB, Image4 BLOB, SavedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)");
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
    }
	
}
