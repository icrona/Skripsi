using UnityEngine;
using System.Collections;

public class UpAndDown : MonoBehaviour {
    public GameObject[] theCake;
    private float[] initialPosY;
    
    void Start()
    {
        theCake = new GameObject[transform.childCount];
        initialPosY = new float[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i] = transform.GetChild(i).gameObject;
            initialPosY[i] = theCake[i].transform.localPosition.y;
        }
    }
	public void upCake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i].transform.position += Vector3.up;
        }
    }

    public void downCake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i].transform.position -= Vector3.up;
        }
    }

    public void resetPosY()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i].transform.localPosition=new Vector3(0,initialPosY[i],0);
        }
    }
}
