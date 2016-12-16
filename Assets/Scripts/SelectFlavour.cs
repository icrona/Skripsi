using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectFlavour : MonoBehaviour {

    // Use this for initialization
    private int tier;
    public GameObject cakeBasic;
    private int selectedFlavour;
    public Texture2D[] flavourTexture;
    public Dropdown flavour;
    private string[] value;

	void Start () {
        flavourTexture = (Texture2D[])Resources.LoadAll<Texture2D>("Textures");
        //dont forget add available shape
        for (int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = flavourTexture[0];
        }
        tier = transform.GetSiblingIndex()+1;
        selectedFlavour = 0;

        value = new string[PlayerPrefs.GetInt("NumOfFlavour")];
        for(int i = 0; i < value.Length; i++)
        {
            value[i] = PlayerPrefs.GetString("Flavour" + i);
        }

        for (int i = 0; i < value.Length; i++)
        {
            flavour.options.Add(new Dropdown.OptionData() { text = value[i] });
        }
        flavour.value = 0;
        flavour.RefreshShownValue();
    }

    public void selectFlavour(int value)
    {
        selectedFlavour = value;
        PlayerPrefs.SetInt("FlavourTier" + tier, selectedFlavour);
        transform.parent.GetComponent<CakePrice>().calculateCakePrice();
    }
    void Update()
    {  
        if (cakeBasic.activeSelf)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = flavourTexture[0];
                transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            }      
        }
    }
}
