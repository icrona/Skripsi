using UnityEngine;
using System.Collections;

public class CakeSelection : MonoBehaviour {

    private GameObject[] cakeList;
    private int index;
    private void Start()
    {
        cakeList = new GameObject[transform.childCount];
        for (int i=0; i<transform.childCount;i++)
        {
            cakeList[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject go in cakeList)
        {
            go.SetActive(false);
        }
        if (cakeList[0]) 
        {
            cakeList[0].SetActive(true);
        }
    }

    public void toggleLeft()
    {
        cakeList[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = cakeList.Length - 1;
        }
        cakeList[index].SetActive(true);
    }
    public void toggleRight()
    {
        cakeList[index].SetActive(false);
        index++;
        if (index == cakeList.Length)
        {
            index = 0;
        }
        cakeList[index].SetActive(true);
    }
    public void hideCake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cakeList[i].SetActive(false);
        }
    }
    public void showCake()
    {
        cakeList[index].SetActive(true);
    }
}
