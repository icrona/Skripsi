using UnityEngine;
using System.Collections;

public class AddText : MonoBehaviour
{

    // Use this for initialization
    public GameObject textPrefab;
    private GameObject text;
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
        text = Instantiate(textPrefab, new Vector3(0f, -10f, 180f), Quaternion.identity) as GameObject;
        transform.parent.rotation = Quaternion.Euler(90, 180, 0);
        text.transform.parent = transform.GetChild(index).GetChild(2);
        text.transform.localRotation = Quaternion.Euler(90, 180, 0);
    }

}