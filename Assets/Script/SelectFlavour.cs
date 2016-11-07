using UnityEngine;
using System.Collections;

public class SelectFlavour : MonoBehaviour {

    // Use this for initialization
    private int tier;
    public GameObject cakeBasic;
    private int selectedFlavour;
    public Texture2D[] flavourTexture;
	void Start () {
        flavourTexture = (Texture2D[])Resources.LoadAll<Texture2D>("Textures");
        for (int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = flavourTexture[0];
        }
        tier = transform.GetSiblingIndex()+1;
        selectedFlavour = 0;
	}

    public void selectFlavour(int value)
    {
        selectedFlavour = value;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = flavourTexture[value];
        }
    }
    void Update()
    {
        PlayerPrefs.SetInt("FlavourTier" + tier, selectedFlavour);
        if (cakeBasic.activeSelf)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.mainTexture = flavourTexture[PlayerPrefs.GetInt("FlavourTier"+tier)];
                transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 1, 1);
            }
        }
    }
}
