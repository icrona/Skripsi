using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Data;
using Mono.Data.Sqlite;
using SimpleJSON;
using UnityEngine.UI;

public class LoadDatabase : MonoBehaviour {

    // Use this for initialization
    private string connectionString;
    private string filepath;
    private string localVersion;

    private string serverVersion;
    private string urlConfig;
    private JSONNode config;

    private string logoURL;
    public Image logo;

    private TextAsset defaultJSON;
    private string defaultJSONcontent;

    public Text infoText;
    public Button okay;
    public Button signature;

    public int min_days;

    public int flavourCount;
    public string []flavourName;
    public int[] flavourPrice;

    public int sizeCount;
    public int[] sizeSize;
    public float[] sizeRate;

    public int[] shapeAvailability;
    public int[] frostingOne;
    public int[] frostingTwo;
    public int[] frostingThree;
    public int[] frostingFour;
    public int[] frostingAvailability;

    public int[] pipeTopPrice;
    public string pipeTopAvailability;

    public int[] pipeEdgePrice;
    public string pipeEdgeAvailability;

    public int[] sprinklePrice;
    public int[] sprinkleAvailability;

    public int[] candlePrice;
    public string candleAvailability;

    public int[] figurePrice;
    public string figureAvailability;

    void Start () {
        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
        urlConfig = "http://www.skripsweet.xyz/api/manage/config";
        logoURL = "http://www.skripsweet.xyz/images/logo/logo.png";


        if (!File.Exists(filepath))
        {
            Debug.Log("First Time");
            writeFirstJSON();
            PlayerPrefs.SetInt("UserData", 0);
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Music",1);
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    string sqlQuery = String.Format("CREATE TABLE main.Cake (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE , Name TEXT NOT NULL , NumOfTier INTEGER NOT NULL, Size1 INTEGER NOT NULL, Size2 INTEGER, Size3 INTEGER, Frosting TEXT NOT NULL, Flavour1 TEXT NOT NULL, Flavour2 TEXT, Flavour3 TEXT,Price INTEGER NOT NULL, Image1 BLOB, Image2 BLOB, Image3 BLOB, Image4 BLOB, SavedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)");
                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();

                    string query = String.Format("CREATE TABLE main.Version (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE, Version TEXT NOT NULL)");
                    dbCmd.CommandText = query;
                    dbCmd.ExecuteScalar();

                    string defaultVersion = String.Format("INSERT INTO main.Version (Version) VALUES ('Initial Version')");
                    dbCmd.CommandText = defaultVersion;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
        StartCoroutine(Camera.main.GetComponent<CheckConnection>().checkInternetConnection((isConnected) => {
            testConnection(isConnected);
        }));

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
    }
    void updateVersion()
    {
        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string getVersion = String.Format("UPDATE Version Set Version=\"{0}\" WHERE ID=1",serverVersion);
                dbCmd.CommandText = getVersion;
                dbCmd.ExecuteReader();
                dbConnection.Close();
            }
        }
    }

    IEnumerator getLogo()
    {
        Texture2D tex;
        tex = new Texture2D(800, 800, TextureFormat.DXT1, false);
        WWW www = new WWW(logoURL);
        yield return www;
        www.LoadImageIntoTexture(tex);
        Rect rec = new Rect(0, 0, tex.width, tex.height);
        logo.GetComponent<Image>().sprite = Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f));
        Color color = logo.color;
        color.a = 1;
        logo.color = color;
    }

    void testConnection(bool connect)
    {
        if (connect)
        {
            StartCoroutine(getLogo());
            StartCoroutine(getJSON());
            infoText.text = "Okay";
            infoText.transform.parent.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Connection", 1);
        }
        else
        {
            getDefaultJSON();
            infoText.text = "No Internet Connection, You Can Not Open Signature Cake Menu And Order Cake, Click Okay To Continue Using This Apps";
            okay.gameObject.SetActive(true);
            signature.interactable = false;
            PlayerPrefs.SetInt("Connection", 0);
        }
    }
    void getDefaultJSON()
    {
        defaultJSONcontent = File.ReadAllText(Application.persistentDataPath+"/json.txt");
        processJSON(defaultJSONcontent);
    }

    void writeFirstJSON()
    {
        defaultJSON = (TextAsset)Resources.Load("json", typeof(TextAsset));
        defaultJSONcontent = defaultJSON.text;
        File.WriteAllText(Application.persistentDataPath + "/json.txt", defaultJSONcontent);
    }

    IEnumerator getJSON()
    {
        WWW www = new WWW(urlConfig);
        yield return www;
        if (www.error == null)
        {
            processJSON(www.text);
        }
    }
    private void processJSON(string json)
    {
        config = JSON.Parse(json);
        serverVersion = config["version"];
        if (serverVersion != localVersion)
        {
            min_days = config["min_days"].AsInt;
            flavourCount = config["flavour"].Count;
            flavourName = new string[flavourCount];
            flavourPrice = new int[flavourCount];
            for (int i = 0; i < flavourCount; i++)
            {
                flavourName[i] = config["flavour"][i]["name"];
                flavourPrice[i] = config["flavour"][i]["price"].AsInt;
            }

            sizeCount = config["size"].Count;
            sizeSize = new int[sizeCount];
            sizeRate = new float[sizeCount];
            for (int i = 0; i < sizeCount; i++)
            {
                sizeSize[i] = config["size"][i]["size"].AsInt;
                sizeRate[i] = config["size"][i]["rate"].AsFloat;
            }

            shapeAvailability = new int[3];
            for (int i = 0; i < 3; i++)
            {
                shapeAvailability[i] = config["shape"][i]["availability"].AsInt;
            }

            frostingAvailability = new int[3];
            frostingOne = new int[3];
            frostingTwo = new int[3];
            frostingThree = new int[3];
            frostingFour = new int[3];
            for (int i = 0; i < 3; i++)
            {
                frostingAvailability[i] = config["frosting"][i]["availability"].AsInt;
                frostingOne[i] = config["frosting"][i]["one"].AsInt;
                frostingTwo[i] = config["frosting"][i]["two"].AsInt;
                frostingThree[i] = config["frosting"][i]["three"].AsInt;
                frostingFour[i] = config["frosting"][i]["four"].AsInt;
            }
            pipeTopPrice = new int[13];
            for (int i = 0; i < 13; i++)
            {
                pipeTopPrice[i] = config["pipe_top"][i]["price"].AsInt;
                pipeTopAvailability += config["pipe_top"][i]["availability"];
            }

            pipeEdgePrice = new int[15];
            for (int i = 0; i < 15; i++)
            {
                pipeEdgePrice[i] = config["pipe_edge"][i]["price"].AsInt;
                pipeEdgeAvailability += config["pipe_edge"][i]["availability"];
            }

            sprinklePrice = new int[2];
            sprinkleAvailability = new int[2];
            for (int i = 0; i < 2; i++)
            {
                sprinklePrice[i] = config["sprinkle"][i]["price"].AsInt;
                sprinkleAvailability[i] = config["sprinkle"][i]["availability"].AsInt;
            }

            candlePrice = new int[11];
            for (int i = 0; i < 11; i++)
            {
                candlePrice[i] = config["candle"][i]["price"].AsInt;
                candleAvailability += config["candle"][i]["availability"];
            }

            figurePrice = new int[14];
            for (int i = 0; i < 14; i++)
            {
                figurePrice[i] = config["figure"][i]["price"].AsInt;
                figureAvailability += config["figure"][i]["availability"];
            }

            if (serverVersion != localVersion)
            {
                File.WriteAllText(Application.persistentDataPath + "/json.txt", json);
            }
            setPlayerPrefs();
            updateVersion();
        }
        
    }

    void setPlayerPrefs()
    {
        PlayerPrefs.SetInt("MinDays", min_days);
        PlayerPrefs.SetInt("NumOfFlavour", flavourCount);
        for(int i = 0; i < flavourCount; i++)
        {
            PlayerPrefs.SetString("Flavour"+i, flavourName[i]);
            PlayerPrefs.SetInt("FlavourPrice" + i, flavourPrice[i]);
        }

        PlayerPrefs.SetInt("NumOfSize", sizeCount);
        for (int i = 0; i < sizeCount; i++)
        {
            PlayerPrefs.SetInt("Size" + i, sizeSize[i]);
            PlayerPrefs.SetFloat("SizeRate" + i, sizeRate[i]);
        }

        for(int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("Shape" + i, shapeAvailability[i]);
            PlayerPrefs.SetInt("Frosting" + i, frostingAvailability[i]);
            PlayerPrefs.SetInt("FrostingOne" + i, frostingOne[i]);
            PlayerPrefs.SetInt("FrostingTwo" + i, frostingTwo[i]);
            PlayerPrefs.SetInt("FrostingThree" + i, frostingThree[i]);
            PlayerPrefs.SetInt("FrostingFour" + i, frostingFour[i]);
        }
        PlayerPrefs.SetString("PipeTop", pipeTopAvailability);
        for(int i = 0; i < 13; i++)
        {
            PlayerPrefs.SetInt("PipeTop" + i, pipeTopPrice[i]);
        }
        PlayerPrefs.SetString("PipeEdge", pipeEdgeAvailability);
        for (int i = 0; i < 15; i++)
        {
            PlayerPrefs.SetInt("PipeEdge" + i, pipeEdgePrice[i]);
        }
        for(int i = 0; i < 2; i++)
        {
            PlayerPrefs.SetInt("SprinkleAvailability" + i, sprinkleAvailability[i]);
            PlayerPrefs.SetInt("SprinklePrice" + i, sprinklePrice[i]);
        } 
        PlayerPrefs.SetString("Candle", candleAvailability);
        for (int i = 0; i < 11; i++)
        {
            PlayerPrefs.SetInt("Candle" + i, candlePrice[i]);
        }
        PlayerPrefs.SetString("Figure", figureAvailability);
        for (int i = 0; i < 14; i++)
        {
            PlayerPrefs.SetInt("Figure" + i, figurePrice[i]);
        }
    }

}
