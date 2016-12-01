using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectSprinkleSide : MonoBehaviour {

    // Use this for initialization
    private Button[] sprinkle;
    public GameObject sprinkles;
    public int numOfSprinkle;
    public int current;
    public GameObject selectedSprinkle;
    private int tier;
    void Start () {
        numOfSprinkle = sprinkles.transform.childCount;
        selectedSprinkle = new GameObject();
        sprinkle = new Button[numOfSprinkle];
        current = 0;
        for (int i = 0; i < numOfSprinkle; i++)
        {
            sprinkle[i] = sprinkles.transform.GetChild(i).GetComponent<Button>();
            Button b = sprinkle[i];
            AddListener(b, i);
        }
        tier = transform.GetSiblingIndex() + 1;
    }

    void AddListener(Button b, int i)
    {
        b.onClick.AddListener(() => pickSprinkle(b, i));
    }

    void pickSprinkle(Button b, int x)
    {
        for (int i = 0; i < numOfSprinkle; i++)
        {
            sprinkle[i].interactable = true;
        }
        selectedSprinkle = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(current).gameObject;
        hideSprinkle();
        b.interactable = false;
        if (x != 0)
        {
            current = x-1;
            selectedSprinkle = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(current).gameObject;
            StartCoroutine(sprinkleAnimate(current));
        }
    }

    void hideSprinkle()
    {
        for (int i = 0; i < selectedSprinkle.transform.childCount; i++)
        {
            transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(current).GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator sprinkleAnimate(int current)
    {
        Debug.Log(selectedSprinkle.transform.childCount);
        for (int i = 0; i < selectedSprinkle.transform.childCount; i++)
        {
            transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(current).GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
