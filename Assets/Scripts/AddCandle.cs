﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AddCandle : MonoBehaviour {

    // Use this for initialization
    public GameObject []candlePrefab;
    private GameObject candleInstantiate;
    public int index;

    private Button[] candle;
    public GameObject candles;
    private int numOfCandle;
    void Start()
    {
        numOfCandle = candles.transform.childCount;
        candle = new Button[numOfCandle];
        candlePrefab = new GameObject[numOfCandle];
        candlePrefab = (GameObject[])Resources.LoadAll<GameObject>("Prefab/Candles");
        for (int i = 0; i < numOfCandle; i++)
        {
            candle[i] = candles.transform.GetChild(i).GetComponent<Button>();
            Button b = candle[i];
            AddListener(b, i);
        }
    }

    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => addCandle(i));
    }

    public void addCandle(int x)
    {
        transform.parent.parent.GetComponent<CakeRotate>().enabled = false;
        transform.parent.parent.GetComponent<CakeZoom>().resetScale();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }            
        }
        candleInstantiate = Instantiate(candlePrefab[x], new Vector3(0f, -20f, 180f), Quaternion.Euler(new Vector3(0,180,0))) as GameObject;
        
        transform.parent.rotation = Quaternion.Euler(90, 180, 0);
        candleInstantiate.transform.parent = transform.GetChild(index).GetChild(1);
    }

}