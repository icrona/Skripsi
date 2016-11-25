using UnityEngine;
using System.Collections;

public class SelectShape : MonoBehaviour {

    public GameObject[] cakeList;
    public GameObject[] availableShape;
    public int index;
    private int numOfShape;
    private void Start()
    {
        numOfShape = 0;

        for(int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("Shape"+i)==1)
            {
                numOfShape++;
            }
        }

        cakeList = new GameObject[transform.childCount];

        for (int i=0; i<transform.childCount;i++)
        {
            cakeList[i] = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("Shape" + i) == 0)
            {
                Destroy(cakeList[i]);
            }
        }

        availableShape = new GameObject[numOfShape];
        for (int i = 0, j = 0; i < transform.childCount; i++,j++)
        {
            if (PlayerPrefs.GetInt("Shape" + i) == 1)
            {
                availableShape[j] = cakeList[i];
            }
            else
            {
                j--;
            }         
        }

        foreach (GameObject go in availableShape)
        {
            go.SetActive(false);
        }
        if (availableShape[0]) 
        {
            availableShape[0].SetActive(true);
        }
    }

    public void toggleLeft()
    {
        availableShape[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = availableShape.Length - 1;
        }
        availableShape[index].SetActive(true);
    }
    public void toggleRight()
    {
        availableShape[index].SetActive(false);
        index++;
        if (index == availableShape.Length)
        {
            index = 0;
        }
        availableShape[index].SetActive(true);
    }
    public void hideCake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            availableShape[i].SetActive(false);
        }
    }
    public void showCake()
    {
        availableShape[index].SetActive(true);
    }
}
