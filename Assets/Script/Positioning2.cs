using UnityEngine;
using System.Collections;

public class Positioning2 : MonoBehaviour
{
    private float height,tier2height;
    private int index;
    // Use this for initialization
    void Update()
    {
        height = transform.GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.GetChild(0).localScale.y;
        for (int i=0;i<transform.parent.parent.GetChild(1).childCount;i++)
        {
            if (transform.parent.parent.GetChild(1).GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }
        }
        tier2height=transform.parent.parent.GetChild(1).GetChild(index).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.parent.parent.GetChild(1).GetChild(index).GetChild(0).localScale.y ;
        transform.parent.localPosition = new Vector3(0, height+tier2height, 0);
    }


}

