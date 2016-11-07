using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectSize : MonoBehaviour {

    // Use this for initialization
    private int tier;
    private int selectedSize;
    private GameObject []cakeShape;
    private float[] initialScaleX;
    private float[] initialScaleY;
    private float[] initialScaleZ;

    private float []value;
    public Dropdown size;
    private int defaultSize;
    private float scale;
    void Start () {
        cakeShape = new GameObject[transform.childCount];      
        initialScaleX = new float[transform.childCount];
        initialScaleY = new float[transform.childCount];
        initialScaleZ = new float[transform.childCount];

        value = new float[3];
        value[0] = 6;
        value[1] = 8;
        value[2] = 10;

        for (int i=0;i<transform.childCount;i++)
        {
            cakeShape[i] = transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            initialScaleX[i] = cakeShape[i].transform.GetChild(0).localScale.x;
            initialScaleY[i] = cakeShape[i].transform.GetChild(0).localScale.y;
            initialScaleZ[i] = cakeShape[i].transform.GetChild(0).localScale.z;
        }

        for (int i = 0; i < value.Length; i++)
        {
            size.options.Add(new Dropdown.OptionData() { text = value[i] + " inch" });
        }
        defaultSize = value.Length / 2;
        size.value = defaultSize;
        size.RefreshShownValue();
        tier = transform.GetSiblingIndex() + 1;
        selectedSize = defaultSize;
    }
    	
    public void selectSize(int size)
    {
        selectedSize = size;
        for (int i = 0; i < transform.childCount; i++)
        {
            scale = (value[size] / value[defaultSize]);
            cakeShape[i].transform.GetChild(0).localScale = new Vector3(scale*initialScaleX[i],scale*initialScaleY[i],scale*initialScaleZ[i]);
        }
    }
    void Update()
    {
        PlayerPrefs.SetInt("SizeTier" + tier, selectedSize);
    }
}
