using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Globalization;

public class CakePrice : MonoBehaviour {
    public Text price;
    private float calculatedPrice;
    public GameObject cakeBasic;
    private GameObject[] basics;

    private bool first;

    private int flavourLength;
    private int sizeLength;
    private int[] flavourPrice;
    private float[] sizeRate;

    private int[,] frostingPrice;
    private int[] colorIntensity;

    private int[] sprinklePrice;
    private int[] pipeTopPrice;
    private int[] pipeEdgePrice;

    private int[] candlePrice;
    private int[] figurePrice;
 	// Use this for initialization
	void Start () {
        frostingPrice = new int[3, 4];

        first = false;

        for(int i = 0; i < 3; i++)
        {
            frostingPrice[i, 0] = PlayerPrefs.GetInt("FrostingOne" + i);
            frostingPrice[i, 1] = PlayerPrefs.GetInt("FrostingTwo" + i);
            frostingPrice[i, 2] = PlayerPrefs.GetInt("FrostingThree" +i);
            frostingPrice[i, 3] = PlayerPrefs.GetInt("FrostingFour" + i);
        }
        frostingPrice[1, 2] = frostingPrice[1, 0];
        colorIntensity = new int[2];
        candlePrice = new int[11];
        figurePrice = new int[14];
    }

    public int[] getCandlePrice()
    {
        for(int i = 0; i < 10; i++)
        {
            candlePrice[i+1] = PlayerPrefs.GetInt("Candle" + i);
        }
        candlePrice[0]= PlayerPrefs.GetInt("Candle10");
        return candlePrice;
    }
    public int[] getFigurePrice()
    {
        for (int i = 0; i < 13; i++)
        {
            figurePrice[i] = PlayerPrefs.GetInt("Figure" + i);
        }
        return figurePrice;
    }

    void prepareInitial()
    {
        flavourLength = PlayerPrefs.GetInt("NumOfFlavour");
        sizeLength = PlayerPrefs.GetInt("NumOfSize");

        flavourPrice = new int[flavourLength];
        sizeRate = new float[sizeLength];

        for (int i = 0; i < flavourLength; i++)
        {
            flavourPrice[i] = PlayerPrefs.GetInt("FlavourPrice" + i);
        }
        for (int i = 0; i < sizeLength; i++)
        {
            sizeRate[i] = PlayerPrefs.GetFloat("SizeRate" + i);
        }
        sprinklePrice = new int[3];
        sprinklePrice[0] = 0;
        for (int i = 1; i <= 2; i++)
        {
            sprinklePrice[i] = PlayerPrefs.GetInt("SprinklePrice" + (i - 1));
        }

        pipeTopPrice = new int[14];
        pipeTopPrice[0] = 0;
        for (int i = 1; i <= 13; i++)
        {
            pipeTopPrice[i] = PlayerPrefs.GetInt("PipeTop" + (i - 1));
        }

        pipeEdgePrice = new int[16];
        pipeEdgePrice[0] = 0;
        for (int i = 1; i <= 15; i++)
        {
            pipeEdgePrice[i] = PlayerPrefs.GetInt("PipeEdge" + (i - 1));
        }
    }

    void beautifyPrice()
    {
        PlayerPrefs.SetInt("CakePrice", Convert.ToInt32(calculatedPrice.ToString("0")));
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
        price.text = String.Format(elGR, "{0:0,0}", calculatedPrice);
    }
    public void substractCakePrice(string minusprice)
    {
        calculatedPrice -= Convert.ToInt32(minusprice);
        beautifyPrice();
    }
    public void calculateCakePrice()
    {
        calculatedPrice = 0;
        if (PlayerPrefs.GetInt("Frosting") != -1)
        {
            for (int i = 1; i <= PlayerPrefs.GetInt("NumberOfTiers"); i++)
            {
                calculatedPrice += (flavourPrice[PlayerPrefs.GetInt("FlavourTier" + i)] * sizeRate[PlayerPrefs.GetInt("SizeTier" + i)]) +
                    (frostingPrice[PlayerPrefs.GetInt("Frosting"), PlayerPrefs.GetInt("FrostingTier" + i)] * sizeRate[PlayerPrefs.GetInt("SizeTier" + i)]) +
                    (sprinklePrice[PlayerPrefs.GetInt("SprinkleTopTier" + i)] + sprinklePrice[PlayerPrefs.GetInt("SprinkleSideTier" + i)]) +
                    (pipeTopPrice[PlayerPrefs.GetInt("PipeTopTier" + i)] + pipeEdgePrice[PlayerPrefs.GetInt("PipeEdgeTier" + i)] + pipeEdgePrice[PlayerPrefs.GetInt("PipeBottomTier" + i)]);
                for (int j = 0; j < 3; j++)
                {
                    if (transform.GetChild(i - 1).GetChild(j).gameObject.activeSelf)
                    {
                        for (int k = 0; k < transform.GetChild(i - 1).GetChild(j).GetChild(1).childCount; k++)
                        {
                            calculatedPrice += Convert.ToInt32(transform.GetChild(i - 1).GetChild(j).GetChild(1).GetChild(k).name);
                        }
                    }
                }
            }
        }
        else
        {
            if (first == false)
            {
                prepareInitial();
            }
            for (int i = 1; i <= PlayerPrefs.GetInt("NumberOfTiers"); i++)
            {
                calculatedPrice += (flavourPrice[PlayerPrefs.GetInt("FlavourTier" + i)] * sizeRate[PlayerPrefs.GetInt("SizeTier" + i)]) +
                    (sprinklePrice[PlayerPrefs.GetInt("SprinkleTopTier" + i)] + sprinklePrice[PlayerPrefs.GetInt("SprinkleSideTier" + i)]) +
                    (pipeTopPrice[PlayerPrefs.GetInt("PipeTopTier" + i)] + pipeEdgePrice[PlayerPrefs.GetInt("PipeEdgeTier" + i)] + pipeEdgePrice[PlayerPrefs.GetInt("PipeBottomTier" + i)]);
                for (int j = 0; j < 3; j++)
                {
                    if (transform.GetChild(i - 1).GetChild(j).gameObject.activeSelf)
                    {
                        for (int k = 0; k < transform.GetChild(i - 1).GetChild(j).GetChild(1).childCount; k++)
                        {
                            calculatedPrice += Convert.ToInt32(transform.GetChild(i - 1).GetChild(j).GetChild(1).GetChild(k).name);
                        }
                    }
                }
            }
            first = true;
        }
        beautifyPrice();
    }
}