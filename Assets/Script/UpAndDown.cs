using UnityEngine;
using System.Collections;

public class UpAndDown : MonoBehaviour {
    public GameObject[] theCake;
    
    void Start()
    {
        theCake = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            theCake[i] = transform.GetChild(i).gameObject;
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
}
