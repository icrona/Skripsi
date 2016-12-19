using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class CakeData : MonoBehaviour
{
    private string connectionString;
    private string filepath;
    private byte[] bytes1;
    private byte[] bytes2;
    private byte[] bytes3;
    private byte[] bytes4;
    public InputField cakeName;

    private string cName;
    private int numTier;
    private int[] cSize;
    private string cFrosting;
    private string[] cFlavour;
    private int cPrice;

    private string[] frostingName;

    public GameObject cakeNamePanel;
    int show;
    private Vector3 showPos;
    private Vector3 hidePos;
    private float speed = 1000;
    public GameObject warningPanel;
    public Text warningText;
    public GameObject cake;
    public GameObject[] theCake;
    private bool empty;
    public Button order;
    public bool saved;
    private int selectedCakeID;

    public Text previewSize;
    public Text previewFlavour;
    public Text previewFrosting;
    public Text previewPrice;

    public GameObject pricePanel;

    void Start()
    {
        cSize = new int[3];
        cFlavour = new string[3];
        frostingName = new string[3];
        frostingName[0] = "Butter Cream";
        frostingName[1] = "Ganache";
        frostingName[2] = "Icing";
        hidePos = new Vector3(587.5f, 0, 0);
        showPos = new Vector3(187.5f, 0, 0);
        theCake = new GameObject[cake.transform.childCount];
        for (int i = 0; i < cake.transform.childCount; i++)
        {
            theCake[i] = cake.transform.GetChild(i).gameObject;
        }

        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
        saved = false;
    }
    private void SaveCake(string name, int numTier, int size1, int size2, int size3, string frosting,string flavour1, string flavour2, string flavour3, int price, byte[] bytes1, byte[] bytes2, byte[] bytes3, byte[] bytes4)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO Cake(Name,NumOfTier,Size1,Size2,Size3,Frosting,Flavour1,Flavour2,Flavour3,Price,Image1,Image2,Image3,Image4,SavedDate) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",@image1,@image2,@image3,@image4,datetime(CURRENT_TIMESTAMP,'localtime'))", name, numTier,size1, size2, size3, frosting, flavour1, flavour2, flavour3,price, bytes1, bytes2, bytes3, bytes4);
                SqliteParameter parameter = new SqliteParameter("@image1", System.Data.DbType.Binary);
                parameter.Value = bytes1;
                dbCmd.Parameters.Add(parameter);
                parameter = new SqliteParameter("@image2", System.Data.DbType.Binary);
                parameter.Value = bytes2;
                dbCmd.Parameters.Add(parameter);

                parameter = new SqliteParameter("@image3", System.Data.DbType.Binary);
                parameter.Value = bytes3;
                dbCmd.Parameters.Add(parameter);

                parameter = new SqliteParameter("@image4", System.Data.DbType.Binary);
                parameter.Value = bytes4;
                dbCmd.Parameters.Add(parameter);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();

                string getLastId= "SELECT last_insert_rowid()";
                dbCmd.CommandText = getLastId;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        selectedCakeID = reader.GetInt32(0);
                    }
                }
                dbConnection.Close();
            }
        }
        PlayerPrefs.SetInt("selectedCakeID", selectedCakeID);
        PlayerPrefs.SetString("OrderFrom", "Apps");
    }

    public void showCakeNamePanel()
    {
        takeScreenshot();
    }
    public void hideCakeNamePanel()
    {
        show = -1;
    }
    public void submitCakeName()
    {
        if (cakeName.text == "")
        {
            warningPanel.SetActive(true);
            cakeNamePanel.SetActive(false);
            empty = true;
        }
        else
        {
            collectData();            
            warningText.text = "Your cake has been saved, you can order now or later from gallery menu";
            warningPanel.SetActive(true);
            hideCakeNamePanel();
            empty = false;
            SaveCake(cName,numTier,cSize[0], cSize[1], cSize[2],cFrosting, cFlavour[0],cFlavour[1],cFlavour[2],cPrice, bytes1, bytes2, bytes3, bytes4);
            if (PlayerPrefs.GetInt("Connection") == 1)
            {
                order.GetComponent<Button>().interactable = true;
            }       
            saved = true;
        }
    }
    public void warningOk()
    {
        warningPanel.SetActive(false);
        if (empty == true)
        {
            cakeNamePanel.SetActive(true);
        }
    }
    void collectData()
    {
        cName = cakeName.text;
        numTier = PlayerPrefs.GetInt("NumberOfTiers");
        for(int i = 0; i < 3; i++)
        {
            cSize[i] = 0;
            cFlavour[i] = "";
        }
        for(int i = 0,j=1; i < numTier; i++,j++)
        {
            cSize[i] = PlayerPrefs.GetInt("Size"+PlayerPrefs.GetInt("SizeTier" + j));
            cFlavour[i] = PlayerPrefs.GetString("Flavour" + PlayerPrefs.GetInt("FlavourTier" + j));
        }
        cFrosting = frostingName[PlayerPrefs.GetInt("Frosting")];
        cPrice = PlayerPrefs.GetInt("CakePrice");
    }

    public void showPreviewData()
    {
        pricePanel.SetActive(false);
        collectData();
        for (int i = 0; i < numTier; i++)
        {
            if (i != 0)
            {
                previewSize.text += ", ";
                previewFlavour.text += ", ";
            }
            previewSize.text += cSize[i]+"cm";          
            previewFlavour.text += cFlavour[i];
            
        }
        previewFrosting.text=cFrosting;
        
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
        
        previewPrice.text = "Rp. " +
            String.Format(elGR, "{0:0,0}", cPrice);
    }

    public void resetPreviewData()
    {
        pricePanel.SetActive(false);
        previewSize.text = "";
        previewFlavour.text = "";
        previewPrice.text = "";
        previewFrosting.text = "";
    }
    void Update()
    {        
        float step = speed * Time.deltaTime;
        if (show == 1)
        {
            cakeNamePanel.transform.localPosition = Vector3.MoveTowards(cakeNamePanel.transform.localPosition, showPos, step);
        }
        if (show == -1)
        {
            cakeNamePanel.transform.localPosition = Vector3.MoveTowards(cakeNamePanel.transform.localPosition, hidePos, step);
        }
    }
    public void takeScreenshot()
    {
        StartCoroutine(screenshot());
    }
    IEnumerator screenshot()
    {
        yield return new WaitForEndOfFrame();
        int width = 5 * Screen.width / 7;
        int height = Screen.height / 2;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(Screen.width / 7, Screen.height / 3, width, height), 0, 0);
        tex.Apply();
        bytes1 = tex.EncodeToPNG();
        Destroy(tex);

        for (int i = 0; i < cake.transform.childCount; i++)
        {
            theCake[i].transform.localRotation = Quaternion.Euler(-45, 0, 0);
        }
        yield return new WaitForEndOfFrame();

        tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(Screen.width / 7, Screen.height / 3, width, height), 0, 0);
        tex.Apply();
        bytes2 = tex.EncodeToPNG();
        Destroy(tex);


        for (int i = 0; i < cake.transform.childCount; i++)
        {
            theCake[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        yield return new WaitForEndOfFrame();

        tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(Screen.width / 7, Screen.height / 3, width, height), 0, 0);
        tex.Apply();
        bytes3 = tex.EncodeToPNG();
        Destroy(tex);


        for (int i = 0; i < cake.transform.childCount; i++)
        {
            theCake[i].transform.localRotation = Quaternion.Euler(90, 180, 0);
        }
        yield return new WaitForEndOfFrame();

        tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(Screen.width / 7, Screen.height / 3, width, height), 0, 0);
        tex.Apply();
        bytes4 = tex.EncodeToPNG();
        Destroy(tex);

        for (int i = 0; i < cake.transform.childCount; i++)
        {
            theCake[i].transform.localRotation = Quaternion.Euler(45, 180, 0);
        }
        show = 1;
    }

}
