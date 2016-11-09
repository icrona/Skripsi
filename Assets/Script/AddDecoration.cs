using UnityEngine;
using System.Collections;

public class AddDecoration : MonoBehaviour {

    // Use this for initialization
    public GameObject decorationPrefab;
    private GameObject decoration;
    public int index;

    public void add()
    {
        transform.parent.parent.GetComponent<CakeRotate>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }
            
        }
        decoration = Instantiate(decorationPrefab, new Vector3(0f, -20f, 180f), Quaternion.identity) as GameObject;
        transform.parent.rotation = Quaternion.Euler(90, 180, 0);
        decoration.transform.parent = transform.GetChild(index).GetChild(1);
        decoration.transform.localRotation = Quaternion.Euler(90, 180, 0);
        decoration.transform.localScale = new Vector3(3,3,3);
    }

}
