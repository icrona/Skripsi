using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TierManagement : MonoBehaviour {

    // Use this for initialization
    public Text tierText;
    public Button upTier;
    public Button downTier;
    public GameObject tier1;
    public GameObject tier2;
    public GameObject tier3;

    public GameObject CakeBasic;
    public GameObject FrostingColor;
    public GameObject Decoration;
    private int index;
    private int tiers;

    public void up()
    {
        downTier.interactable = true;
        CakeBasic.transform.GetChild(index).gameObject.SetActive(false);
        FrostingColor.transform.GetChild(index).gameObject.SetActive(false);
        Decoration.transform.GetChild(index).gameObject.SetActive(false);
        index++;
        if (index == tiers)
        {
            upTier.interactable = false;
        }
        CakeBasic.transform.GetChild(index).gameObject.SetActive(true);
        FrostingColor.transform.GetChild(index).gameObject.SetActive(true);
        Decoration.transform.GetChild(index).gameObject.SetActive(true);
        updateText();
    }
    public void down()
    {
        upTier.interactable = true;
        CakeBasic.transform.GetChild(index).gameObject.SetActive(false);
        FrostingColor.transform.GetChild(index).gameObject.SetActive(false);
        Decoration.transform.GetChild(index).gameObject.SetActive(false);
        index--;
        if (index == 0)
        {
            downTier.interactable = false;
        }
        CakeBasic.transform.GetChild(index).gameObject.SetActive(true);
        FrostingColor.transform.GetChild(index).gameObject.SetActive(true);
        Decoration.transform.GetChild(index).gameObject.SetActive(true);
        updateText();
    }
    public void updateText()
    {
        tierText.text = (index + 1).ToString();
    }
    public void getTiers(int tier)
    {
        tiers = tier;
        if (tiers>0)
        {
            upTier.interactable = true;
        }
        if (index >= tier)
        {
            int a = index-tier;
            for(int i=0;i< a; i++)
            {
                down();
            }
            upTier.interactable = false;
        }
    }
    void Update()
    {
        if (tiers == 0)
        {
            tier2.SetActive(false);
            tier3.SetActive(false);
        }
        if (tiers == 1)
        {
            tier2.SetActive(true);
            tier3.SetActive(false);
        }
        if (tiers == 2)
        {
            tier2.SetActive(true);
            tier3.SetActive(true);
        }
    }

}

