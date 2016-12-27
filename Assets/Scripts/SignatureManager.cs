using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using System.Linq;
using System.Globalization;
using System;

public class SignatureManager : MonoBehaviour
{
    private JSONNode signatureCake;
    private int[] cakeCount;
    private int[,] cakeId;
    private string[,] cakeName;
    private string[,] cakeDescription;
    private int[,] cakeSize;
    private int[,] cakePrice;
    private string[,] cakeImage;

    private Texture2D[,] tex;
    private Sprite[,] cakes;
    private string url = "http://www.skripsweet.xyz/api/signature";
    private string imageURL = "http://www.skripsweet.xyz/images";

    public GameObject gridPanel;
    private GameObject[] panel;
    private GameObject[] gridContainer;
    public GameObject gridPrefab;
    private int index;

    private int []indexDetail;

    public GameObject detailPanel;
    public GameObject[] detail;
    public GameObject[] detailContainer;
    public GameObject cakeContents;
    CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

    private int counter;
    public GameObject loading;

    private Text[] cakeIndex;
    void Start()
    {
        counter = -1;
        cakeCount = new int[3];
        panel = new GameObject[3];
        gridContainer = new GameObject[3];
        detail = new GameObject[3];
        detailContainer = new GameObject[3];
        for(int i = 0; i < 3; i++)
        {
            panel[i] = gridPanel.transform.GetChild(i).gameObject;
            gridContainer[i] = gridPanel.transform.GetChild(i).GetChild(2).GetChild(0).gameObject;
            detail[i] = detailPanel.transform.GetChild(i).gameObject;
            detailContainer[i] = detail[i].transform.GetChild(0).gameObject;
        }
        index = 0;
        indexDetail = new int[3];
        cakeIndex = new Text[3];
        for(int i = 0; i < 3; i++)
        {
            cakeIndex[i]=detailPanel.transform.GetChild(i).GetChild(1).GetChild(2).GetComponent<Text>();
        }
        StartCoroutine(GetJSON());
    }
    IEnumerator GetJSON() {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            processJSON(www.text);
        }
    }
    void processJSON(string json)
    {
        signatureCake = JSON.Parse(json);
        for(int i = 0; i < 3; i++)
        {
            cakeCount[i] = signatureCake[i].Count;
        }
        for(int i = 0; i < 3; i++)
        {
            cakeIndex[i].text = "1/" + cakeCount[i];
        }
        int max = cakeCount.Max();
        cakeId = new int[3, max];
        cakeName = new string[3, max];
        cakeDescription = new string[3, max];
        cakeSize = new int[3, max];
        cakePrice = new int[3, max];
        cakeImage = new string[3, max];
        tex = new Texture2D[3, max];
        cakes = new Sprite[3, max];

        counter = cakeCount[0] + cakeCount[1] + cakeCount[2];
       
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < cakeCount[i]; j++)
            {
                cakeId[i, j] = signatureCake[i][j]["id"].AsInt;
                cakeName[i,j] = signatureCake[i][j]["name"];
                cakeDescription[i, j] = signatureCake[i][j]["description"];
                cakeSize[i, j] = signatureCake[i][j]["size"].AsInt;
                cakePrice[i, j] = signatureCake[i][j]["price"].AsInt;
                cakeImage[i, j] = signatureCake[i][j]["image"];               
                StartCoroutine(getImage(cakeImage[i, j], i, j));

            }
            instantiatePrevNext(i);
            instantiateOrderBtn(i);
        }
    }

    IEnumerator getImage(string image,int i,int j)
    {
        tex[i, j] = new Texture2D(400, 400, TextureFormat.DXT1,false);
        string url = imageURL + '/' + image;
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(tex[i,j]);
        cakes[i, j] = Sprite.Create(tex[i, j], new Rect(0, 0, 400, 400), new Vector2(0.5f, 0.5f));
        showGrid(i, j);
        generateDetails(i, j);
        instantiateBackButton(i);
        counter--;      
    }
    void Update()
    {
        if (counter == 0)
        {
            loading.SetActive(false);
            counter = -1;
        }
    }
    void instantiateOrderBtn(int i)
    {
        detailPanel.transform.GetChild(i).GetChild(1).GetChild(3).GetComponent<Button>().onClick.AddListener(() => orderCake(i));
    }
    void instantiateBackButton(int i)
    {
        detailPanel.transform.GetChild(i).GetChild(3).GetComponent<Button>().onClick.AddListener(()=> backToGrid());
    }
    void instantiatePrevNext(int i)
    {
        detailPanel.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => togglePrev(i));
        detailPanel.transform.GetChild(i).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => toggleNext(i));
    }
    void orderCake(int i)
    {
        for(int x=0; x < cakeCount[i]; x++)
        {
            if (detailContainer[i].transform.GetChild(x).gameObject.activeSelf)
            {
                int selected = Convert.ToInt32(detailContainer[i].transform.GetChild(x).name);
                PlayerPrefs.SetInt("selectedCakeID",cakeId[i, selected]);
                PlayerPrefs.SetString("OrderFrom", "Signature");
                PlayerPrefs.SetString("SignatureName", cakeName[i, selected]);
                PlayerPrefs.SetInt("SignatureSize", cakeSize[i, selected]);
                PlayerPrefs.SetInt("SignaturePrice", cakePrice[i, selected]);
                PlayerPrefs.SetString("SignatureImage", cakeImage[i, selected]);
                break;
            }
        }
        transform.GetComponent<SignatureConfirmation>().showConfirmOrder();
    }
    void togglePrev(int i)
    {
        if (indexDetail[i] == 0)
        {
            indexDetail[i] = cakeCount[i]-1;
        }
        else
        {
            indexDetail[i]--;
        }
        changePrevNext(indexDetail[i],i);
    }
    void toggleNext(int i)
    {
        if (indexDetail[i] == cakeCount[i]-1)
        {
            indexDetail[i] = 0;
        }
        else
        {
            indexDetail[i]++;
        }    
        changePrevNext(indexDetail[i],i);
    }
    void changePrevNext(int indexDetail, int i)
    {
        for(int x = 0; x < cakeCount[i]; x++)
        {
            detailContainer[i].transform.GetChild(x).gameObject.SetActive(false);
        }
        detailContainer[i].transform.GetChild(indexDetail).gameObject.SetActive(true);
        cakeIndex[i].text = (indexDetail + 1) + "/" + cakeCount[i];
    }
    
    void backToGrid()
    {
        Camera.main.GetComponent<ButtonSound>().playSound();
        gridPanel.SetActive(true);
        detailPanel.SetActive(false);
    }
    void showGrid(int i,int j)
    {
        GameObject thumbnail = Instantiate(gridPrefab) as GameObject;
        thumbnail.GetComponent<Image>().sprite = cakes[i, j];
        thumbnail.transform.SetParent(gridContainer[i].transform, false);
        thumbnail.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => showCakeDetail(i,j));
    }
    void generateDetails(int i,int j)
    {
        GameObject cakeContent = Instantiate(cakeContents) as GameObject;
        cakeContent.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = cakes[i, j];
        cakeContent.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = cakeName[i, j];
        cakeContent.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = cakeDescription[i, j];
        cakeContent.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "Size: "+cakeSize[i, j]+"cm";
        cakeContent.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "Price: Rp. "+ String.Format(elGR, "{0:0,0}", cakePrice[i, j]);
        cakeContent.transform.SetParent(detailContainer[i].transform, false);
        cakeContent.name = j.ToString();
        if (j!=0) {
            cakeContent.SetActive(false);
        }
    }

    void showCakeDetail(int i,int j)
    {
        gridPanel.SetActive(false);
        detailPanel.SetActive(true);
        for(int x = 0; x < detailContainer[i].transform.childCount; x++)
        {
            detailContainer[i].transform.GetChild(x).gameObject.SetActive(false);
            if (detailContainer[i].transform.GetChild(x).name == j.ToString())
            {
                detailContainer[i].transform.GetChild(x).gameObject.SetActive(true);
                indexDetail[i] = x;
                cakeIndex[i].text = (x + 1) + "/" + cakeCount[i];
            }
        }             
    }

    public void left()
    {
        if (index == 0)
        {
            index = 2;
        }
        else
        {
            index--;
        }
        changePanel(index);
    }
    public void right()
    {
        if (index == 2)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        changePanel(index);
    }

    void changePanel(int index)
    {
        for(int i = 0; i < 3; i++)
        {
            gridPanel.transform.GetChild(i).gameObject.SetActive(false);
            detailPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        gridPanel.transform.GetChild(index).gameObject.SetActive(true);
        detailPanel.transform.GetChild(index).gameObject.SetActive(true);
    }

}
