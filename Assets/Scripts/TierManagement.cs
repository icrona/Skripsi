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
    public GameObject Text;
    private int index;
    private int tiers;


    public void up()
    {
        downTier.interactable = true;
        deactivate();
        index++;
        if (index == tiers)
        {
            upTier.interactable = false;
        }
        activate();
        updateText();        
    }
    public void down()
    {
        upTier.interactable = true;
        deactivate();
        index--;
        if (index == 0)
        {
            downTier.interactable = false;
        }
        activate();
        updateText();

    }
    private void activate()
    {
        CakeBasic.transform.GetChild(index).gameObject.SetActive(true);
        FrostingColor.transform.GetChild(index).gameObject.SetActive(true);
        Decoration.transform.GetChild(index).gameObject.SetActive(true);
        Text.transform.GetChild(index).gameObject.SetActive(true);
    }
    private void deactivate()
    {
        CakeBasic.transform.GetChild(index).gameObject.SetActive(false);
        FrostingColor.transform.GetChild(index).gameObject.SetActive(false);
        Decoration.transform.GetChild(index).gameObject.SetActive(false);
        Text.transform.GetChild(index).gameObject.SetActive(false);
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
        PlayerPrefs.SetInt("NumberOfTiers", tier+1);
    }
    void Update()
    {
        if (tiers == 0)
        {
            tier2.SetActive(false);
            tier3.SetActive(false);
            PlayerPrefs.SetInt("IsThere2Tiers", 0);
            PlayerPrefs.SetInt("IsThere3Tiers", 0);
            PlayerPrefs.SetInt("SizeTier2", -1);
            PlayerPrefs.SetInt("SizeTier3", -1);
        }
        if (tiers == 1)
        {
            tier2.SetActive(true);
            tier3.SetActive(false);
            PlayerPrefs.SetInt("IsThere3Tiers", 0);
            PlayerPrefs.SetInt("SizeTier3", -1);
        }
        if (tiers == 2)
        {
            tier2.SetActive(true);
            tier3.SetActive(true);
        }
    }

}

