using UnityEngine;
using System.Collections;

public class DraggerForText : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 objectPosition;
    public float height, height2;
    public int tier;
    public int index;
    public float distance;
    void Start()
    {
        tier = transform.parent.parent.parent.GetSiblingIndex();
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
        transform.parent.parent.parent.parent.parent.GetComponent<CakeZoom>().enabled = false;
        transform.parent.parent.parent.parent.rotation = Quaternion.Euler(90, 180, 0);
    }
    void OnMouseDrag()
    {
        transform.parent.parent.parent.parent.parent.GetComponent<CakeRotate>().enabled = false;
        transform.parent.parent.parent.parent.parent.GetComponent<CakeZoom>().enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = Quaternion.Euler(90, 180, 0);
        height = transform.parent.parent.parent.GetChild(index).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.parent.parent.parent.GetChild(index).GetChild(0).localScale.y;
        if (tier == 2)
        {
            height2 = transform.parent.parent.parent.parent.GetChild(1).GetChild(index).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.parent.parent.parent.parent.GetChild(1).GetChild(index).GetChild(0).localScale.y;
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
                distance = 180f - height - height2;
                break;
        }

        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objectPosition;
    }

}

