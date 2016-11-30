using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AddDecoration : MonoBehaviour {

    // Use this for initialization
    public GameObject []decorationPrefab;
    private GameObject decorationInstantiate;
    private GameObject[] list;
    public int index;

    private Button[] decoration;
    public GameObject decorations;
    private int numOfDecoration;
    void Start()
    {  
        numOfDecoration = decorations.transform.childCount;
        decoration = new Button[numOfDecoration];
        decorationPrefab = new GameObject[numOfDecoration];
        decorationPrefab = (GameObject[])Resources.LoadAll<GameObject>("Prefab/Candles");
        for (int i = 0; i < numOfDecoration; i++)
        {
            decoration[i] = decorations.transform.GetChild(i).GetComponent<Button>();
            Button b = decoration[i];
            AddListener(b, i);
        }
    }

    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => addDecoration(i));
    }

    public void addDecoration(int x)
    {
        transform.parent.parent.GetComponent<CakeRotate>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index = i;
            }            
        }        
        decorationInstantiate = Instantiate(decorationPrefab[x], new Vector3(0f, -20f, 180f), Quaternion.identity) as GameObject;
        
        transform.parent.rotation = Quaternion.Euler(90, 180, 0);
        
        decorationInstantiate.transform.parent = transform.GetChild(index).GetChild(1);
    }

}
