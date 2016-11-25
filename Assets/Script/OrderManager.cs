using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;
public class OrderManager : MonoBehaviour {

    public GameObject customerData;
    public GameObject confirmation;

    private string connectionString;
    private string filepath;

    private string nameCake;
    private int numTier;
    private string[] size;
    private string[] flavour;
    private string frosting;
    private string price;

    private byte[] image1;
    private byte[] image2;
    private byte[] image3;
    private byte[] image4;
    public GameObject imagePanel;
    public GameObject []cakeImage;

    public Text custNameText;
    public Text custPhoneText;
    public Text custEmailText;
    public Text custDateText;
    public Text custAddressText;
    public Text custNotesText;

    public GameObject cakeData;
    public GameObject[] cakeDataByNumTier;
    public InputField enterName;
    public InputField enterPhone;
    public InputField enterEmail;
    public InputField enterDate;
    public InputField enterAddress;
    public InputField enterNotes;


    private Texture2D[] tex;
    int width;
    int height;

    public Toggle useMyData;
    private bool saveData;
    private bool useData;

    void Start()
    {
        size = new string[3];
        flavour = new string[3];
        saveData = true;
        useData = false;
        int selectedCakeID = PlayerPrefs.GetInt("selectedCakeID");
        width = 5 * Screen.width / 7;
        height = Screen.height / 2;
        cakeImage = new GameObject[imagePanel.transform.childCount];
        tex = new Texture2D[imagePanel.transform.childCount];

        for (int i = 0; i < 4; i++)
        {
            cakeImage[i] = imagePanel.transform.GetChild(i).gameObject;
            tex[i] = new Texture2D(width, height, TextureFormat.RGB24, false);
        }
        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = String.Format("SELECT * FROM Cake Where ID=\"{0}\"", selectedCakeID.ToString());
                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nameCake = reader.GetString(1);
                        numTier = reader.GetInt32(2);  
                        size[0] = reader.GetString(3);
                        size[1] = reader.GetString(4);
                        size[2] = reader.GetString(5);
                        frosting = reader.GetString(6);
                        flavour[0] = reader.GetString(7);
                        flavour[1] = reader.GetString(8);
                        flavour[2] = reader.GetString(9);
                        price = reader.GetString(10);
                        image1 = (byte[])reader["Image1"];
                        image2 = (byte[])reader["Image2"];
                        image3 = (byte[])reader["Image3"];
                        image4 = (byte[])reader["Image4"];
                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        cakeDataByNumTier = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            cakeDataByNumTier[i] = cakeData.transform.GetChild(i).gameObject;
        }
        cakeDataByNumTier[numTier - 1].SetActive(true);
        switch (numTier)
        {
            case 1:
                cakeDataByNumTier[numTier - 1].transform.GetChild(0).GetComponent<Text>().text = nameCake;
                cakeDataByNumTier[numTier - 1].transform.GetChild(1).GetComponent<Text>().text = flavour[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(2).GetComponent<Text>().text = size[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(3).GetComponent<Text>().text = frosting;
                cakeDataByNumTier[numTier - 1].transform.GetChild(4).GetComponent<Text>().text = price;
                break;
            case 2:
                cakeDataByNumTier[numTier - 1].transform.GetChild(0).GetComponent<Text>().text = nameCake;
                cakeDataByNumTier[numTier - 1].transform.GetChild(1).GetComponent<Text>().text = "Tier 1: " + flavour[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(2).GetComponent<Text>().text = "Tier 2: " + flavour[1];
                cakeDataByNumTier[numTier - 1].transform.GetChild(3).GetComponent<Text>().text = "Tier 1: " +size[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(4).GetComponent<Text>().text = "Tier 2: " + size[1];
                cakeDataByNumTier[numTier - 1].transform.GetChild(5).GetComponent<Text>().text = frosting;
                cakeDataByNumTier[numTier - 1].transform.GetChild(6).GetComponent<Text>().text = price;
                break;
            case 3:
                cakeDataByNumTier[numTier - 1].transform.GetChild(0).GetComponent<Text>().text = nameCake;
                cakeDataByNumTier[numTier - 1].transform.GetChild(1).GetComponent<Text>().text = "Tier 1: " + flavour[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(2).GetComponent<Text>().text = "Tier 2: " + flavour[1];
                cakeDataByNumTier[numTier - 1].transform.GetChild(3).GetComponent<Text>().text = "Tier 3: " + flavour[2];
                cakeDataByNumTier[numTier - 1].transform.GetChild(4).GetComponent<Text>().text = "Tier 1: " + size[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(5).GetComponent<Text>().text = "Tier 2: " + size[1];
                cakeDataByNumTier[numTier - 1].transform.GetChild(6).GetComponent<Text>().text = "Tier 3: " + size[2];
                cakeDataByNumTier[numTier - 1].transform.GetChild(7).GetComponent<Text>().text = frosting;
                cakeDataByNumTier[numTier - 1].transform.GetChild(8).GetComponent<Text>().text = price;
                break;
        }
        tex[0].LoadImage(image1);
        tex[1].LoadImage(image2);
        tex[2].LoadImage(image3);
        tex[3].LoadImage(image4);

        for (int i = 0; i < imagePanel.transform.childCount; i++)
        {
            cakeImage[i].GetComponent<Image>().sprite = Sprite.Create(tex[i], new Rect(0, 0, width, height), new Vector2(0f, 0f));
        }
        if (PlayerPrefs.GetInt("UserData")==0)
        {
            useMyData.interactable = false;
        }
    }
    public void goConfirm()
    {
        customerData.SetActive(false);
        confirmation.SetActive(true);
        if(saveData == true)
        {
            PlayerPrefs.SetString("CustomerName", enterName.text);
            PlayerPrefs.SetString("CustomerPhone", enterPhone.text);
            PlayerPrefs.SetString("CustomerEmail", enterEmail.text);
            PlayerPrefs.SetString("CustomerAddress", enterAddress.text);
            PlayerPrefs.SetInt("UserData", 1);
        }

        custNameText.text = enterName.text;
        custPhoneText.text = enterPhone.text;
        custEmailText.text = enterEmail.text;
        custDateText.text = enterDate.text;
        custAddressText.text = enterAddress.text;
        custNotesText.text = enterNotes.text;
    }

    public void saveOrNot()
    {
        saveData=!saveData;
    }
    public void useOrNot()
    {
        useData = !useData;
    }
    public void back()
    {
        if (customerData.activeSelf)
        {
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            confirmation.SetActive(false);
            customerData.SetActive(true);
        }
    }

    void Update()
    {
        if (useData)
        {
            enterName.text = PlayerPrefs.GetString("CustomerName");
            enterPhone.text = PlayerPrefs.GetString("CustomerPhone");
            enterEmail.text = PlayerPrefs.GetString("CustomerEmail");
            enterAddress.text = PlayerPrefs.GetString("CustomerAddress");
        }
        else
        {
            enterName.text = "";
            enterPhone.text = "";
            enterEmail.text = "";
            enterAddress.text = "";
        }
    }
}
