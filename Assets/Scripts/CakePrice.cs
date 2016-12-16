using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CakePrice : MonoBehaviour {
    public Text price;
    private float calculatedPrice;
    public GameObject cakeBasic;
    private GameObject[] basics;

    private int flavourLength;
    private int sizeLength;
    private int[] flavourPrice;
    private float[] sizeRate;

    private int[,] frostingPrice;
    private int[] colorIntensity;
	// Use this for initialization
	void Start () {

        flavourLength = PlayerPrefs.GetInt("NumOfFlavour");
        sizeLength = PlayerPrefs.GetInt("NumOfSize");

        flavourPrice = new int[flavourLength];
        sizeRate = new float[sizeLength];

        for(int i = 0; i < flavourLength; i++)
        {
            flavourPrice[i] = PlayerPrefs.GetInt("FlavourPrice" + i);
        }
        for(int i = 0; i < sizeLength; i++)
        {
            sizeRate[i] = PlayerPrefs.GetFloat("SizeRate" + i);
        }

        frostingPrice = new int[3, 4];

        for(int i = 0; i < 3; i++)
        {
            frostingPrice[i, 0] = PlayerPrefs.GetInt("FrostingOne" + i);
            frostingPrice[i, 1] = PlayerPrefs.GetInt("FrostingTwo" + i);
            frostingPrice[i, 2] = PlayerPrefs.GetInt("FrostingThree" +i);
            frostingPrice[i, 3] = PlayerPrefs.GetInt("FrostingFour" + i);
        }
        frostingPrice[1, 2] = frostingPrice[1, 0];
        colorIntensity = new int[2];     
    }

    public void calculateCakePrice()
    {
        calculatedPrice = 0;

        if (PlayerPrefs.GetInt("Frosting") != -1)
        {
            for (int i = 1; i <= PlayerPrefs.GetInt("NumberOfTiers"); i++)
            {
                calculatedPrice += (flavourPrice[PlayerPrefs.GetInt("FlavourTier" + i)] * sizeRate[PlayerPrefs.GetInt("SizeTier" + i)]) + 
                    (frostingPrice[PlayerPrefs.GetInt("Frosting"), PlayerPrefs.GetInt("FrostingTier"+i)] * sizeRate[PlayerPrefs.GetInt("SizeTier" + i)]);
            }
        }
        else
        {
            for (int i = 1; i <= PlayerPrefs.GetInt("NumberOfTiers"); i++)
            {
                calculatedPrice += (flavourPrice[PlayerPrefs.GetInt("FlavourTier" + i)] * sizeRate[PlayerPrefs.GetInt("SizeTier" + i)]);
            }
        }


        price.text = calculatedPrice.ToString("0");

    }
}