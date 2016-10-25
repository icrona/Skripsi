using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using System.IO;
public class CakeData : MonoBehaviour
{
    private string connectionString;
    private string filepath;
    private byte[] bytes1;
    private byte[] bytes2;
    private byte[] bytes3;
    private byte[] bytes4;
    public Dropdown selectSize;
    public Dropdown selectFlavour;
    public InputField cakeName;
    private string cName;
    private string cSize;
    private string cFlavour;

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

    void Start()
    {
        hidePos = new Vector3(587.5f, 0, 0);
        showPos = new Vector3(187.5f, 0, 0);
        theCake = new GameObject[cake.transform.childCount];
        for (int i = 0; i < cake.transform.childCount; i++)
        {
            theCake[i] = cake.transform.GetChild(i).gameObject;
        }

        filepath = Application.persistentDataPath + "/CakeDB.sqlite";
        connectionString = "URI=file:" + filepath;
    }
    private void SaveCake(string name, string size, string flavour, byte[] bytes1, byte[] bytes2, byte[] bytes3, byte[] bytes4)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {

                string sqlQuery = String.Format("INSERT INTO Cake(Name,Size,Flavour,Image1,Image2,Image3,Image4,SavedDate) VALUES(\"{0}\",\"{1}\",\"{2}\",@image1,@image2,@image3,@image4,datetime(CURRENT_TIMESTAMP,'localtime'))", name, size, flavour, bytes1, bytes2, bytes3, bytes4);
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
                dbConnection.Close();
            }
        }
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
            warningText.text = "Your cake has been saved, you can order now or later from gallery menu";
            warningPanel.SetActive(true);
            hideCakeNamePanel();
            empty = false;
            SaveCake(cName, cSize, cFlavour, bytes1, bytes2, bytes3, bytes4);
            order.GetComponent<Button>().interactable = true;

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
    void Update()
    {
        cName = cakeName.text;
        cFlavour = selectFlavour.captionText.text;
        cSize = selectSize.captionText.text;
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
