using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Globalization;

public class OrderManager : MonoBehaviour {

    public GameObject customerData;
    public GameObject confirmation;

    private string connectionString;
    private string filepath;

    private string nameCake;
    private int numTier;
    private int[] size;
    private string[] flavour;
    private string frosting;
    private int price;

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

    public GameObject loading;
    public GameObject okay;
    public Text loadingText;


    private string urlOrder = "http://www.skripsweet.xyz/api/order";

    void Start()
    {
        size = new int[3];
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
                        size[0] = reader.GetInt32(3);
                        size[1] = reader.GetInt32(4);
                        size[2] = reader.GetInt32(5);
                        frosting = reader.GetString(6);
                        flavour[0] = reader.GetString(7);
                        flavour[1] = reader.GetString(8);
                        flavour[2] = reader.GetString(9);
                        price = reader.GetInt32(10);
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
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
        switch (numTier)
        {
            case 1:
                cakeDataByNumTier[numTier - 1].transform.GetChild(0).GetComponent<Text>().text = nameCake;
                cakeDataByNumTier[numTier - 1].transform.GetChild(1).GetComponent<Text>().text = flavour[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(2).GetComponent<Text>().text = size[0].ToString()+" cm";
                cakeDataByNumTier[numTier - 1].transform.GetChild(3).GetComponent<Text>().text = frosting;
                cakeDataByNumTier[numTier - 1].transform.GetChild(4).GetComponent<Text>().text = "Rp. "+ String.Format(elGR, "{0:0,0}", price);
                break;
            case 2:
                cakeDataByNumTier[numTier - 1].transform.GetChild(0).GetComponent<Text>().text = nameCake;
                cakeDataByNumTier[numTier - 1].transform.GetChild(1).GetComponent<Text>().text = "Tier 1: " + flavour[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(2).GetComponent<Text>().text = "Tier 2: " + flavour[1];
                cakeDataByNumTier[numTier - 1].transform.GetChild(3).GetComponent<Text>().text = "Tier 1: " +size[0] + " cm";
                cakeDataByNumTier[numTier - 1].transform.GetChild(4).GetComponent<Text>().text = "Tier 2: " + size[1] + " cm";
                cakeDataByNumTier[numTier - 1].transform.GetChild(5).GetComponent<Text>().text = frosting;
                cakeDataByNumTier[numTier - 1].transform.GetChild(6).GetComponent<Text>().text = "Rp. "+ String.Format(elGR, "{0:0,0}", price);
                break;
            case 3:
                cakeDataByNumTier[numTier - 1].transform.GetChild(0).GetComponent<Text>().text = nameCake;
                cakeDataByNumTier[numTier - 1].transform.GetChild(1).GetComponent<Text>().text = "Tier 1: " + flavour[0];
                cakeDataByNumTier[numTier - 1].transform.GetChild(2).GetComponent<Text>().text = "Tier 2: " + flavour[1];
                cakeDataByNumTier[numTier - 1].transform.GetChild(3).GetComponent<Text>().text = "Tier 3: " + flavour[2];
                cakeDataByNumTier[numTier - 1].transform.GetChild(4).GetComponent<Text>().text = "Tier 1: " + size[0] + " cm";
                cakeDataByNumTier[numTier - 1].transform.GetChild(5).GetComponent<Text>().text = "Tier 2: " + size[1] + " cm";
                cakeDataByNumTier[numTier - 1].transform.GetChild(6).GetComponent<Text>().text = "Tier 3: " + size[2] + " cm";
                cakeDataByNumTier[numTier - 1].transform.GetChild(7).GetComponent<Text>().text = frosting;
                cakeDataByNumTier[numTier - 1].transform.GetChild(8).GetComponent<Text>().text = "Rp. "+ String.Format(elGR, "{0:0,0}", price);
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
        if (PlayerPrefs.GetInt("UserData")==1)
        {
            useMyData.interactable = true;
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
        Debug.Log(saveData);
    }
    public void isUseData(bool on)
    {
        if (on) {
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

    public void order()
    {
        loading.SetActive(true);
        
        StartCoroutine(Camera.main.GetComponent<CheckConnection>().checkInternetConnection((isConnected) => {
            check(isConnected);
        }));
        
    }
    void check(bool on)
    {
        if (on)
        {
            loadingText.text = "Loading ....";
            okay.SetActive(false);
            StartCoroutine(sendOrder());
        }
        else
        {
            loadingText.text = "No Internet Connection";
            okay.SetActive(true);
        }
    }
    IEnumerator sendOrder()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", custNameText.text);
        form.AddField("phone", custPhoneText.text);
        form.AddField("email", custEmailText.text);
        form.AddField("date", custDateText.text);
        form.AddField("address", custAddressText.text);
        form.AddField("notes", custNotesText.text);
        form.AddField("cake_name", nameCake);
        form.AddField("cake_tier", numTier);

        form.AddField("cake_size", size[0]);
        form.AddField("cake_size1", size[1]);
        form.AddField("cake_size2", size[2]);

        form.AddField("cake_flavour", flavour[0]);
        form.AddField("cake_flavour1", flavour[1]);
        form.AddField("cake_flavour2", flavour[2]);

        form.AddField("cake_frosting", frosting);

        form.AddBinaryData("image1", image1,"screenshot1.png","image/png");
        form.AddBinaryData("image2", image2, "screenshot2.png", "image/png");
        form.AddBinaryData("image3", image3, "screenshot3.png", "image/png");
        form.AddBinaryData("image4", image4, "screenshot4.png", "image/png");


        form.AddField("cake_price", price);
        form.AddField("status", "Waiting Confirmation");
        form.AddField("order_from", "Apps");

        WWW w = new WWW(urlOrder, form);
        yield return w;
        if (!string.IsNullOrEmpty(w.error))
        {
            print(w.error);
        }
        else
        {
            loadingText.text = "Finished Ordering";
            okay.SetActive(true);
        }

    }
    public void close()
    {
        loading.SetActive(false);
    }
}
