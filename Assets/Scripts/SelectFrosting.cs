using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SelectFrosting : MonoBehaviour {
    public GameObject frosting;
    public GameObject cakeBasic;
    public Toggle[] selectFrosting;
    public GameObject colorPanel;
    private int selectedFrosting;
    public Texture2D[] frostingTextures;
    public Button next;
    // Use this for initialization
    void Start () {
        colorPanel.SetActive(false);
        selectFrosting = new Toggle[frosting.transform.childCount];
        for (int i=0;i<frosting.transform.childCount;i++)
        {
            if (PlayerPrefs.GetInt("Frosting"+i)==1)
            {
                selectFrosting[i] = frosting.transform.GetChild(i).GetComponent<Toggle>();
                Toggle t = selectFrosting[i];
                AddListener(t, i);
            }
            else
            {
                frosting.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
        frostingTextures = (Texture2D[])Resources.LoadAll<Texture2D>("Textures");
    }
    void AddListener(Toggle t,int i)
    {
        t.onValueChanged.AddListener(delegate { pickFrosting(true,i); } );
    }
    void pickFrosting(bool value,int i)
    {
        if (i == 0)
        {
            frostingButterCream();
            PlayerPrefs.SetInt("Frosting", 0);
        }

        else if (i == 1)
        {
            frostingGanache();
            PlayerPrefs.SetInt("Frosting", 1);
        }

        else if (i==2)
        {
            frostingIcing();
            PlayerPrefs.SetInt("Frosting", 2);
        }
        next.gameObject.SetActive(true);
        transform.parent.GetComponent<CakePrice>().calculateCakePrice();
    }
	
    private void frostingButterCream()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = frostingTextures[1];
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        colorPanel.SetActive(true);
    }

    private void frostingIcing()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture =null;
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        colorPanel.SetActive(true);
    }
    private void frostingGanache()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = frostingTextures[2];
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        colorPanel.SetActive(false);
    }
    void Update()
    {
        if (cakeBasic.activeSelf)
        {
            selectFrosting = new Toggle[frosting.transform.childCount];
            for (int i = 0; i < frosting.transform.childCount; i++)
            {
                selectFrosting[i] = frosting.transform.GetChild(i).GetComponent<Toggle>();
                selectFrosting[i].isOn = false; 
            }
            PlayerPrefs.SetInt("Frosting" , -1);
        }
    }
}
