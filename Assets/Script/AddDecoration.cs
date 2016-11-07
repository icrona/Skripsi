using UnityEngine;
using System.Collections;

public class AddDecoration : MonoBehaviour {

    // Use this for initialization
    public GameObject cake;
    public GameObject decorationPrefab;
    private GameObject decoration;
    public float [,]height;
    public int index;
    public int tier;
	void Start () {
        height = new float[3,transform.childCount];
        tier = transform.GetSiblingIndex();
	}

    public void add()
    {
        transform.parent.parent.GetComponent<CakeRotate>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }
            height[index, i] = transform.GetChild(i).GetChild(0).GetComponent<MeshFilter>().mesh.bounds.size.y * transform.GetChild(i).GetChild(0).localScale.y;
        }
        decoration = Instantiate(decorationPrefab, new Vector3(0f, -20f, 180f), Quaternion.identity) as GameObject;
        cake.transform.rotation = Quaternion.Euler(90, 180, 0);
        decoration.transform.parent = transform.GetChild(index);
        decoration.transform.localRotation = Quaternion.Euler(90, 180, 0);
        decoration.transform.localScale = new Vector3(3,3,3);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
