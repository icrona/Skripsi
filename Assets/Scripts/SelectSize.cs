using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectSize : MonoBehaviour {

    // Use this for initialization
    private int tier;
    private int selectedSize;
    private GameObject []cakeShape;
    private GameObject[] availableShape;
    private float[] initialScaleX;
    private float[] initialScaleY;
    private float[] initialScaleZ;
    private int numOfShape;
    private float []value;
    public Dropdown size;
    private int defaultSize;
    private float scale;

    void Start () {
        cakeShape = new GameObject[transform.childCount];      
        initialScaleX = new float[transform.childCount];
        initialScaleY = new float[transform.childCount];
        initialScaleZ = new float[transform.childCount];

        value = new float[PlayerPrefs.GetInt("NumOfSize")];
        for (int i=0; i<value.Length;i++)
        {
            value[i] = PlayerPrefs.GetInt("Size" + i);
        }

        PlayerPrefs.SetInt("IsThere2Tiers", 0);
        PlayerPrefs.SetInt("IsThere3Tiers", 0);
        //dont forget add available shape
        for (int i=0;i<transform.childCount;i++)
        {
            cakeShape[i] = transform.GetChild(i).gameObject;
        }

        numOfShape = 0;

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("Shape" + i) == 1)
            {
                numOfShape++;
            }
        }

        cakeShape = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            cakeShape[i] = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("Shape" + i) == 0)
            {
                Destroy(cakeShape[i]);
            }
        }

        availableShape = new GameObject[numOfShape];
        for (int i = 0, j = 0; i < transform.childCount; i++, j++)
        {
            if (PlayerPrefs.GetInt("Shape" + i) == 1)
            {
                availableShape[j] = cakeShape[i];
            }
            else
            {
                j--;
            }
        }

        for (int i = 0; i < numOfShape; i++)
        {
            initialScaleX[i] = availableShape[i].transform.GetChild(0).localScale.x;
            initialScaleY[i] = availableShape[i].transform.GetChild(0).localScale.y;
            initialScaleZ[i] = availableShape[i].transform.GetChild(0).localScale.z;
        }

        for (int i = 0; i < value.Length; i++)
        {
            size.options.Add(new Dropdown.OptionData() { text = value[i] + " cm" });
        }
        defaultSize = value.Length / 2;
        size.value = defaultSize;
        size.RefreshShownValue();
        tier = transform.GetSiblingIndex() + 1;
        
        if (tier == 2)
        {
            adjustSizeTier2();
        }
        else if (tier == 3)
        {
            adjustSizeTier3();
        }
        else
        {
            selectedSize = defaultSize;
        }
    }
    	
    public void selectSize(int size)
    {
        selectedSize = size;
        for (int i = 0; i < numOfShape; i++)
        {
            scale = (value[size] / value[defaultSize]);
            availableShape[i].transform.GetChild(0).localScale = new Vector3(scale*initialScaleX[i],scale*initialScaleY[i],scale*initialScaleZ[i]);
        }
        PlayerPrefs.SetInt("SizeTier" + tier, selectedSize);
        transform.parent.GetComponent<CakePrice>().calculateCakePrice();
    }
    public void selectSize1(int size)
    {
        selectedSize = size;
        for (int i = 0; i < numOfShape; i++)
        {
            scale = (value[size] / value[defaultSize]);
            availableShape[i].transform.GetChild(0).localScale = new Vector3(scale * initialScaleX[i], scale * initialScaleY[i], scale * initialScaleZ[i]);
        }
        PlayerPrefs.SetInt("SizeTier" + tier, selectedSize);
    }
    public void validateSize()
    {
        if (tier == 3 && PlayerPrefs.GetInt("IsThere3Tiers") < 1)
        {
            if (PlayerPrefs.GetInt("SizeTier3")>(PlayerPrefs.GetInt("SizeTier1")))
            {
                PlayerPrefs.SetInt("IsThere3Tiers", 1);
                adjustSizeTier2();
            }
            else
            {
                PlayerPrefs.SetInt("IsThere3Tiers", 1);
                adjustSizeTier3();
            }
        }

        else if (tier == 2 && PlayerPrefs.GetInt("IsThere2Tiers")<1)
        {
            PlayerPrefs.SetInt("IsThere2Tiers", 1);
            adjustSizeTier2();          
        }
    }
    void adjustSizeTier2()
    {
        selectSize1(PlayerPrefs.GetInt("SizeTier1"));
        size.value = selectedSize;
    }

    void adjustSizeTier3()
    {
        adjustSizeTier2();
        selectSize1(PlayerPrefs.GetInt("SizeTier2"));
        size.value = selectedSize;
    }
}
