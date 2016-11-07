using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CakeColor : MonoBehaviour {
    public GameObject []cakeColor;
    public GameObject cakeColorContainer;

	// Use this for initialization
	void Start () {
        Sprite[] textures = Resources.LoadAll<Sprite>("Color");
        cakeColor = new GameObject[cakeColorContainer.transform.childCount];
        
        for (int i=0;i<cakeColorContainer.transform.childCount;i++)
        {
            cakeColor[i] = cakeColorContainer.transform.GetChild(i).gameObject;
            
        }
        for (int i = 1; i < cakeColorContainer.transform.childCount; i++)
        {
            cakeColor[i].GetComponent<Image>().sprite = textures[i-1];
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
