﻿using UnityEngine;
using System.Collections;

public class Dragger : MonoBehaviour {
    Vector3 mousePosition;
    Vector3 objectPosition;
    public float  height,height2;
    public int tier;
    public int index;
    public float distance;
    private bool delete;
    private GameObject deleteDecoration;
    void Start()
    {
        tier = transform.parent.parent.parent.GetSiblingIndex();
        delete = false;
    }
    void Update()
    {
        for (int i = 0; i < transform.parent.parent.parent.childCount; i++)
        {
            if (transform.parent.parent.parent.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }
        }
    }
    void OnMouseDown()
    {
        transform.parent.parent.parent.parent.parent.GetComponent<CakeRotate>().enabled = false;
    }
    void OnMouseUp()
    {
        transform.parent.parent.parent.parent.parent.GetComponent<CakeRotate>().enabled = true;
        if (delete)
        {
            Destroy(gameObject);
            delete = false;
            deleteDecoration.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
    void OnMouseDrag()
    {
        transform.parent.parent.parent.parent.parent.GetComponent<CakeRotate>().enabled = false;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        height = transform.parent.parent.parent.GetChild(index).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.parent.parent.parent.GetChild(index).GetChild(0).localScale.y;
        if (tier == 2)
        {
            height2=transform.parent.parent.parent.parent.GetChild(1).GetChild(index).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.parent.parent.parent.parent.GetChild(1).GetChild(index).GetChild(0).localScale.y;
        }
        switch (tier)
        {
            case 0:
                distance = 180f;
                break;
            case 1:
                distance = 180f - height;
                break;

            case 2:
                distance = 180f - height-height2;
                break;
        }
        
        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,distance);
        objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objectPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DeleteDecoration")
        {
            deleteDecoration = collision.gameObject;
            collision.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            delete = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "DeleteDecoration")
        {
            collision.transform.localScale = new Vector3(1f, 1f, 1f);
            delete = false;
        }
    }

}